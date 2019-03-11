const Recipes = function () {
    const selectTag = document.getElementById('recipeSelector');
    const wantedLitersField = document.getElementById('wantedLiters');
    
    let constants;
    const baseUrl = '/api';
    const recipesBase = `${baseUrl}/Recipes`;

    function toggleLoadingDiv(showLoadDiv){
        document.getElementById('recipeData').style.display = (showLoadDiv ? 'none' : 'block');
        document.getElementById('loadingRecipe').style.display = (showLoadDiv ? 'block' : 'none');
    }

    function deleteCurrentTableRows(tableBody) {
        while (tableBody.hasChildNodes()) {
            tableBody.removeChild(tableBody.lastChild);
        }
    }

    function setValueInSpansWithClassName(className, value) {
        const elements = document.getElementsByClassName(className);
        [].forEach.call(elements, function (el) {
            el.innerHTML = value;
        });
    }

    function createAndAppendTableCell(row, cellValue) {
        const cell = document.createElement('td');
        cell.innerHTML = cellValue;
        row.appendChild(cell);
    }

    function createIngredientRow(scalingFactor, ingredient){
        const row = document.createElement('tr');

        createAndAppendTableCell(row, ingredient.commonData.ingredient.name);
        createAndAppendTableCell(row, ingredient.kilograms);
        createAndAppendTableCell(row, `${ingredient.kilograms} * ${scalingFactor} = ${ingredient.adjustedKilograms}`);

        return row;
    }

    function createMaltRow(scalingFactor, malt) {
        const row = document.createElement('tr');

        createAndAppendTableCell(row, malt.commonData.ingredient.name);
        createAndAppendTableCell(row, `${malt.percentageOfGrainBill}%`);
        createAndAppendTableCell(row, malt.kilograms);
        createAndAppendTableCell(row, `${malt.kilograms} * ${scalingFactor} = ${malt.adjustedKilograms}`);

        return row;
    }

    function createMashStepRow(stepNumber, mashStep) {
        const row = document.createElement('tr');

        createAndAppendTableCell(row, stepNumber);
        createAndAppendTableCell(row, `${mashStep.degreesCelsius} ºC`);
        createAndAppendTableCell(row, `${mashStep.durationInMinutes}`);

        return row;
    }

    // TODO: clean up this file.
    function loadRecipe() {
        let url = `${recipesBase}/${selectTag.value}/`;
        
        if(wantedLitersField.value) {
            url = url.concat(wantedLitersField.value);
        }

        toggleLoadingDiv(true);

        axios.get(url)
            .then(function (response) {
                console.log(response);
                const recipe = response.data;

                if(!wantedLitersField.value) {
                    wantedLitersField.value = recipe.staticValues.liters;
                }

                document.getElementById('recipeTitle').innerHTML = `${recipe.staticValues.name}`;
                document.getElementById('scalingFactor').innerHTML = `${recipe.scalingFactor}`;

                setValueInSpansWithClassName('aantalLiters', recipe.staticValues.liters);
                setValueInSpansWithClassName('gewildeLiters', recipe.litersWanted);
                
                const maltBody = document.getElementById('malts');
                deleteCurrentTableRows(maltBody);

                let totalPercentage = 0;
                recipe.malts.forEach(malt => {
                    const row = createMaltRow(recipe.scalingFactor, malt);
                    totalPercentage += malt.percentageOfGrainBill;
                    maltBody.appendChild(row);
                });

                document.getElementById('totalPercentage').innerHTML = totalPercentage;
                document.getElementById('originalGrainMass').innerHTML = recipe.staticValues.totalGrainMass;
                document.getElementById('adjustedGrainMass').innerHTML = recipe.adjustedGrainMass;
                
                const hopsBody = document.getElementById('hops');
                deleteCurrentTableRows(hopsBody);

                let hopCounter = 0;
                recipe.hops.forEach(hop => {
                    const row = document.createElement('tr');

                    const nameCell = document.createElement('td');
                    nameCell.innerHTML = hop.commonData.ingredient.name;
                    row.appendChild(nameCell);
                    
                    const originalAauCell = document.createElement('td');
                    originalAauCell.innerHTML = `${hop.aau}`;
                    row.appendChild(originalAauCell);

                    const adjustedAauCell = document.createElement('td');
                    adjustedAauCell.innerHTML = `${hop.adjustedAAU}`;
                    row.appendChild(adjustedAauCell);
                    
                    const alphaAcidCell = document.createElement('td');
                    const alphaAcidTextbox = document.createElement('input');
                    alphaAcidTextbox.type = 'number';
                    alphaAcidTextbox.step = '0.1';
                    alphaAcidTextbox.dataset.hopcounter = hopCounter;
                    alphaAcidTextbox.dataset.adjustedAau = hop.adjustedAAU;

                    alphaAcidTextbox.addEventListener('change', function(event) {
                        const hopSpan = document.getElementById(`hop_${event.target.dataset.hopcounter}`);
                        const gramsOfHop = event.target.dataset.adjustedAau / event.target.value * constants.gramsPerOunce;
                        hopSpan.innerHTML = `((${event.target.dataset.adjustedAau} / ${event.target.value}) * ${constants.gramsPerOunce}) = ${gramsOfHop}`;
                    });
                    alphaAcidCell.appendChild(alphaAcidTextbox);
                    row.appendChild(alphaAcidCell);

                    const massCell = document.createElement('td');
                    const massSpan = document.createElement('span');
                    massSpan.id = `hop_${hopCounter}`;
                    massCell.appendChild(massSpan);
                    row.appendChild(massCell);

                    const cookingTimeCell = document.createElement('td');
                    cookingTimeCell.innerHTML = `${hop.cookingTime} minuten`;
                    row.appendChild(cookingTimeCell)

                    hopsBody.appendChild(row);
                    hopCounter++;
                });

                if(recipe.otherIngredients.length == 0) {
                    document.getElementById('noIngredients').style.display = 'block';
                    document.getElementById('extraIngredientsTable').style.display = 'none';
                } else {
                    document.getElementById('noIngredients').style.display = 'none';
                    document.getElementById('extraIngredientsTable').style.display = 'block';

                    const ingredientsBody = document.getElementById('extraIngredients');
                    deleteCurrentTableRows(ingredientsBody);

                    recipe.otherIngredients.forEach(ingredient => {
                        const row = createIngredientRow(recipe.scalingFactor, ingredient);
                        ingredientsBody.appendChild(row);
                    });
                }

                document.getElementById('yeast').innerHTML = `${recipe.staticValues.yeast.manufacturer} - ${recipe.staticValues.yeast.name}`;
                document.getElementById('mashingWater').innerHTML = `${recipe.litersForMashing} liter`;
                document.getElementById('spargeWater').innerHTML = `${recipe.spargeWater} liter`;
                document.getElementById('mashThickness').innerHTML = `${recipe.staticValues.litersPerKiloOfMalt}l / kg`;
                document.getElementById('maltVolume').innerHTML = `${recipe.maltVolume} liter`;
                document.getElementById('totalVolume').innerHTML = `${recipe.litersForMashing} + ${recipe.maltVolume} = ${recipe.litersForMashing + recipe.maltVolume} liter`;
                document.getElementById('mashDescription').innerHTML = recipe.staticValues.mashStepDescription;

                const mashStepsBody = document.getElementById('mashStepsBody');
                deleteCurrentTableRows(mashStepsBody);

                for(let i = 0; i < recipe.mashSteps.length; i++){
                    const row = createMashStepRow(i+1, recipe.mashSteps[i]);
                    mashStepsBody.appendChild(row);
                }

                const volumeBeforeCooking = recipe.litersForMashing + recipe.spargeWater - (recipe.adjustedGrainMass * constants.amountOfWater1KilogramOfMaltAbsorbs);
                document.getElementById('volumeBeforeCooking').innerHTML = `${recipe.litersForMashing} + ${recipe.spargeWater} - (${recipe.adjustedGrainMass} * ${constants.amountOfWater1KilogramOfMaltAbsorbs}) = ${volumeBeforeCooking} liter.`;
                document.getElementById('cookingDescription').innerHTML = recipe.staticValues.cookingStepDescription.replace('$POST_COOK_VOLUME$', `${volumeBeforeCooking} - ${recipe.staticValues.litersEvaporatedAfterCooking} = ${volumeBeforeCooking - recipe.staticValues.litersEvaporatedAfterCooking}`);
                
                document.getElementById('beginSw').innerHTML = recipe.staticValues.beginSpecificWeight;
                document.getElementById('endSw').innerHTML = recipe.staticValues.endSpecificWeight;
                document.getElementById('fermentationDescription').innerHTML = recipe.staticValues.fermentationStepDescription;

                document.getElementById('co2Range').innerHTML = `${recipe.staticValues.minimumCO2} - ${recipe.staticValues.maximumCO2}`;
                document.getElementById('bottlingDescription').innerHTML = recipe.staticValues.bottlingDescription;
                
                toggleLoadingDiv(false);
            });
    }

    function createOption(value, text) {
        const option = document.createElement('option');
        option.value = value;
        option.innerHTML = text;
        return option;
    }

    function loadConstants() {
        return axios.get(`${baseUrl}/Constants`)
        .then(function (response) {
            constants = response.data;
        });
    }

    return {
        init: function () {
            loadConstants()
            .then(function() {
                wantedLitersField.addEventListener('change', loadRecipe);
                selectTag.addEventListener('change', loadRecipe);
    
                axios.get(recipesBase)
                .then(function (response) {
                    for (let i = 0; i < response.data.length; i++) {
                        const recipe = response.data[i];
                        selectTag.appendChild(createOption(recipe.id, recipe.name));
                    }
                });
            });
        }
    }
}();