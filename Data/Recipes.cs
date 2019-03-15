using System.Collections.Generic;
using System.Linq;
using BeerScaler.Dtos;

namespace BeerScaler.Data {
    public sealed class Recipes : List<Recipe> {
        public Recipes() {
            CreateAmberWaves();
            CreateSaison();
            CreateCreamAle();
            CreateBelgianStrongAle();
            CreateAmericanBlonde();
            CreateGermanPilsner();
        }
        
        public Recipe GetRecipe(int id) {
            return this.FirstOrDefault(r => r.StaticValues.Id == id);
        }

        private void CreateAmberWaves() {
            decimal cookingTimeInMinutes = 90;
            var amberWaves = new Recipe(1, "Amber waves", 19, cookingTimeInMinutes);

            amberWaves.Malts.Add(new WeightedIngredient(Malts.MildAleMalt, 4.3m));
            amberWaves.Malts.Add(new WeightedIngredient(Malts.CaramunichTypeII, 0.34m));
            amberWaves.Malts.Add(new WeightedIngredient(Malts.BarkeMunich, 0.227m));
            amberWaves.Malts.Add(new WeightedIngredient(Malts.CaraAroma, 0.113m));

            amberWaves.Hops.Add(new HopIngredient(Hops.Horizon, 6.5m, 60));
            amberWaves.Hops.Add(new HopIngredient(Hops.Cascade, 1.5m, 10));
            amberWaves.Hops.Add(new HopIngredient(Hops.Centennial, 2.25m, 10));
            amberWaves.Hops.Add(new HopIngredient(Hops.Cascade, 1.5m, 0));
            amberWaves.Hops.Add(new HopIngredient(Hops.Centennial, 2.25m, 0));

            amberWaves.StaticValues.Yeast = Yeasts.SafaleUs05;
            amberWaves.StaticValues.BeginSpecificWeight = 1.051m;
            amberWaves.StaticValues.EndSpecificWeight = 1.013m;
            amberWaves.StaticValues.MinimumCO2 = 2m;
            amberWaves.StaticValues.MaximumCO2 = 2.5m;

            amberWaves.AddMashStep(new MashStep(60, 68));

            amberWaves.StaticValues.MashStepDescription = "Verwarm het maischwater tot +- 72 ºC. Vervolgens de mout er aan toevoegen. Daarna maischen volgens schema. <br /> Na het maischen verhitten tot 76 ºC en daarna spoelen met spoelwater van 77 ºC.";
            amberWaves.StaticValues.CookingStepDescription = $"De totale kooktijd is {cookingTimeInMinutes} minuten, dus er verdampt {amberWaves.StaticValues.LitersEvaporatedAfterCooking} liter. Voeg de hoppen toe conform het schema. Na het koken afkoelen tot 19 ºC. Er is nu ongeveer {Constants.PostCookVolume} liter over. Neem een sample van 100ml voor het SG.";
            amberWaves.StaticValues.FermentationStepDescription = "Voeg de gist toe en laat deze vergisten op 19 ºC. Zet de temperatuur zo laag mogelijk wanneer de hoofdvergisting voorbij is en laat het vat ongeveer anderhalve week zo staan. Meet na het vergisten en voor het bottelen de SG weer.";
            amberWaves.StaticValues.BottlingDescription = $"Dit bier moet een CO₂ volume hebben dat tussen de <strong>{amberWaves.StaticValues.MinimumCO2}</strong> en <strong>{amberWaves.StaticValues.MaximumCO2}</strong> ligt. Gebruik <a href=\"https://www.brewersfriend.com/beer-priming-calculator/\" target=\"_blank\">deze calculator</a> om te bepalen hoeveel suiker er nodig is. Na het bottelen twee weken laten nagisten op 19 ºC.";

            Add(amberWaves);
        }

        private void CreateSaison() {
            decimal cookingTimeInMinutes = 90;
            var saison = new Recipe(2, "Saison", 19, cookingTimeInMinutes);

            saison.Malts.Add(new WeightedIngredient(Malts.PilsenMd, 4.25m));
            saison.Malts.Add(new WeightedIngredient(Malts.CaramunichTypeII, 0.3m));
            saison.Malts.Add(new WeightedIngredient(Malts.BarkeMunich, 0.3m));
            saison.Malts.Add(new WeightedIngredient(Malts.WheatMalt, 0.3m));

            saison.Hops.Add(new HopIngredient(Hops.HallertauMittelfruh, 5.64m, 60));
            saison.Hops.Add(new HopIngredient(Hops.HallertauMittelfruh, 3.36m, 0));

            saison.OtherIngredients.Add(new WeightedIngredient(new Ingredient("Suiker"), 0.4m));

            saison.StaticValues.Yeast = Yeasts.Wyeast3724;
            saison.StaticValues.BeginSpecificWeight = 1.055m;
            saison.StaticValues.EndSpecificWeight = 1.006m;
            saison.StaticValues.MinimumCO2 = 2m;
            saison.StaticValues.MaximumCO2 = 2.5m;

            saison.AddMashStep(new MashStep(90, 65));

            saison.StaticValues.MashStepDescription = "Verwarm het maischwater tot +- 70 ºC. Vervolgens de mout er aan toevoegen. Daarna maischen volgens schema. <br /> Na het maischen verhitten tot 76 ºC en daarna spoelen met spoelwater van 77 ºC.";
            saison.StaticValues.CookingStepDescription = $"De totale kooktijd is {cookingTimeInMinutes} minuten, dus er verdampt {saison.StaticValues.LitersEvaporatedAfterCooking} liter. Voeg de hoppen toe conform het schema. De suiker moet 15 minuten voor het einde toegevoegd worden. Na het koken afkoelen tot 19 ºC. Er is nu ongeveer {Constants.PostCookVolume} liter over. Neem een sample van 100ml voor het SG.";
            saison.StaticValues.FermentationStepDescription = "Voeg de gist toe en laat deze vergisten op 19 ºC. Laat na de eerste week de temperatuur langzaam stijgen naar 28 ºC. Meet na het vergisten en voor het bottelen de SG weer.";
            saison.StaticValues.BottlingDescription = $"Dit bier moet een CO₂ volume hebben dat tussen de <strong>{saison.StaticValues.MinimumCO2}</strong> en <strong>{saison.StaticValues.MaximumCO2}</strong> ligt. Gebruik <a href=\"https://www.brewersfriend.com/beer-priming-calculator/\" target=\"_blank\">deze calculator</a> om te bepalen hoeveel suiker er nodig is. Na het bottelen twee weken laten nagisten op 19 ºC.";

            Add(saison);
        }

        private void CreateCreamAle() {
            decimal cookingTimeInMinutes = 90;
            var creamAle = new Recipe(3, "Cream ale", 19, cookingTimeInMinutes);

            creamAle.Malts.Add(new WeightedIngredient(Malts.PaleAleMalt, 2.0m));
            creamAle.Malts.Add(new WeightedIngredient(Malts.PilsenMd, 2.0m));
            creamAle.Malts.Add(new WeightedIngredient(new Ingredient("Rijstvlokken"), 0.8m));

            creamAle.Hops.Add(new HopIngredient(Hops.HallertauTradition, 3.36m, 60));
            creamAle.Hops.Add(new HopIngredient(Hops.HallertauTradition, 1.68m, 0));

            creamAle.StaticValues.Yeast = Yeasts.SafaleUs05;
            creamAle.StaticValues.BeginSpecificWeight = 1.050m;
            creamAle.StaticValues.EndSpecificWeight = 1.009m;
            creamAle.StaticValues.MinimumCO2 = 2.4m;
            creamAle.StaticValues.MaximumCO2 = 2.5m;

            creamAle.AddMashStep(new MashStep(90, 65));

            creamAle.StaticValues.MashStepDescription = "Verwarm het maischwater tot +- 70 ºC. Vervolgens de mout er aan toevoegen. Daarna maischen volgens schema. <br /> Na het maischen alles uitmaischen door te verhitten tot 76 ºC en daarna spoelen met spoelwater van 77 ºC.";
            creamAle.StaticValues.CookingStepDescription = $"De totale kooktijd is {cookingTimeInMinutes} minuten, dus er verdampt {creamAle.StaticValues.LitersEvaporatedAfterCooking} liter. Voeg de hoppen toe conform het schema. Na het koken afkoelen tot 19 ºC. Er is nu ongeveer {Constants.PostCookVolume} liter over. Neem een sample van 100ml voor het SG.";
            creamAle.StaticValues.FermentationStepDescription = "Voeg de gist toe en laat deze vergisten op 18 ºC. Meet na het vergisten en voor het bottelen de SG weer.";
            creamAle.StaticValues.BottlingDescription = $"Dit bier moet een CO₂ volume hebben dat tussen de <strong>{creamAle.StaticValues.MinimumCO2}</strong> en <strong>{creamAle.StaticValues.MaximumCO2}</strong> ligt. Gebruik <a href=\"https://www.brewersfriend.com/beer-priming-calculator/\" target=\"_blank\">deze calculator</a> om te bepalen hoeveel suiker er nodig is. Na het bottelen twee weken laten nagisten op 19 ºC.";

            Add(creamAle);
        }

        private void CreateBelgianStrongAle() {
            decimal cookingTimeInMinutes = 90;
            var beer = new Recipe(4, "Belgian strong ale", 19, cookingTimeInMinutes);  

            beer.Malts.Add(new WeightedIngredient(Malts.PilsenMd, 4.5m));

            beer.Hops.Add(new HopIngredient(Hops.Saaz, 6.5m, 90));

            beer.OtherIngredients.Add(new WeightedIngredient(new Ingredient("Suiker"), 1.12m));

            beer.StaticValues.Yeast = Yeasts.Wyeast1388;
            beer.StaticValues.BeginSpecificWeight = 1.072m;
            beer.StaticValues.EndSpecificWeight = 1.007m;
            beer.StaticValues.MinimumCO2 = 3.9m;
            beer.StaticValues.MaximumCO2 = 4.1m;

            beer.AddMashStep(new MashStep(90, 65));

            beer.StaticValues.MashStepDescription = "Verwarm het maischwater tot +- 70 ºC. Vervolgens de mout er aan toevoegen. Daarna maischen volgens schema. <br /> Na het maischen alles uitmaischen door te verhitten tot 76 ºC en daarna spoelen met spoelwater van 77 ºC.";
            beer.StaticValues.CookingStepDescription = $"De totale kooktijd is {cookingTimeInMinutes} minuten, dus er verdampt {beer.StaticValues.LitersEvaporatedAfterCooking} liter. Voeg de hoppen toe conform het schema. Na het koken afkoelen tot 19 ºC. Er is nu ongeveer {Constants.PostCookVolume} liter over. Neem een sample van 100ml voor het SG.";
            beer.StaticValues.FermentationStepDescription = "Voeg de gist toe op 18 ºC en laat de temperatuur gedurende de eerste week langzaam stijgen naar 28 ºC. Meet na het vergisten en voor het bottelen de SG weer.";
            beer.StaticValues.BottlingDescription = $"Dit bier moet een CO₂ volume hebben dat tussen de <strong>{beer.StaticValues.MinimumCO2}</strong> en <strong>{beer.StaticValues.MaximumCO2}</strong> ligt. Gebruik <a href=\"https://www.brewersfriend.com/beer-priming-calculator/\" target=\"_blank\">deze calculator</a> om te bepalen hoeveel suiker er nodig is. Na het bottelen twee weken laten nagisten op 19 ºC.";

            Add(beer);
        }

        private void CreateAmericanBlonde() {
            decimal cookingTimeInMinutes = 90;
            var beer = new Recipe(5, "American Blonde", 19, cookingTimeInMinutes);  

            beer.Malts.Add(new WeightedIngredient(Malts.PaleAleMalt, 4.53m));
            beer.Malts.Add(new WeightedIngredient(Malts.CaraHell, 0.227m));

            beer.Hops.Add(new HopIngredient(Hops.Tettnanger, 4.1m, 60));

            beer.StaticValues.Yeast = Yeasts.SafaleUs05;
            beer.StaticValues.BeginSpecificWeight = 1.049m;
            beer.StaticValues.EndSpecificWeight = 1.011m;
            beer.StaticValues.MinimumCO2 = 2.4m;
            beer.StaticValues.MaximumCO2 = 2.6m;

            beer.AddMashStep(new MashStep(90, 67));

            beer.StaticValues.MashStepDescription = "Verwarm het maischwater tot +- 72 ºC. Vervolgens de mout er aan toevoegen. Daarna maischen volgens schema. <br /> Na het maischen alles uitmaischen door te verhitten tot 76 ºC en daarna spoelen met spoelwater van 77 ºC.";
            beer.StaticValues.CookingStepDescription = $"De totale kooktijd is {cookingTimeInMinutes} minuten, dus er verdampt {beer.StaticValues.LitersEvaporatedAfterCooking} liter. Voeg de hoppen toe conform het schema. Na het koken afkoelen tot 19 ºC. Er is nu ongeveer {Constants.PostCookVolume} liter over. Neem een sample van 100ml voor het SG.";
            beer.StaticValues.FermentationStepDescription = "Voeg de gist toe op 19 ºC. Meet na het vergisten en voor het bottelen de SG weer.";
            beer.StaticValues.BottlingDescription = $"Dit bier moet een CO₂ volume hebben dat tussen de <strong>{beer.StaticValues.MinimumCO2}</strong> en <strong>{beer.StaticValues.MaximumCO2}</strong> ligt. Gebruik <a href=\"https://www.brewersfriend.com/beer-priming-calculator/\" target=\"_blank\">deze calculator</a> om te bepalen hoeveel suiker er nodig is. Na het bottelen twee weken laten nagisten op 19 ºC.";

            Add(beer);
        }

        private void CreateGermanPilsner() {
            decimal cookingTimeInMinutes = 90;
            var beer = new Recipe(6, "German Pilsner", 19, cookingTimeInMinutes);  

            beer.Malts.Add(new WeightedIngredient(Malts.PilsenMd, 4.4m));

            beer.Hops.Add(new HopIngredient(Hops.Perle, 6.64m, 60));
            beer.Hops.Add(new HopIngredient(Hops.HallertauTradition, 1.68m, 15));
            beer.Hops.Add(new HopIngredient(Hops.HallertauTradition, 1.68m, 1));

            beer.StaticValues.Yeast = Yeasts.Wyeast2124;
            beer.StaticValues.BeginSpecificWeight = 1.048m;
            beer.StaticValues.EndSpecificWeight = 1.009m;
            beer.StaticValues.MinimumCO2 = 2.0m;
            beer.StaticValues.MaximumCO2 = 2.5m;

            beer.AddMashStep(new MashStep(90, 64));

            beer.StaticValues.MashStepDescription = "Verwarm het maischwater tot +- 69 ºC. Vervolgens de mout er aan toevoegen. Daarna maischen volgens schema. <br /> Na het maischen alles uitmaischen door te verhitten tot 76 ºC en daarna spoelen met spoelwater van 77 ºC.";
            beer.StaticValues.CookingStepDescription = $"De totale kooktijd is {cookingTimeInMinutes} minuten, dus er verdampt {beer.StaticValues.LitersEvaporatedAfterCooking} liter. Voeg de hoppen toe conform het schema. Na het koken afkoelen tot 19 ºC. Er is nu ongeveer {Constants.PostCookVolume} liter over. Neem een sample van 100ml voor het SG.";
            beer.StaticValues.FermentationStepDescription = "Afkoelen tot 8 ºC en gedurende drie dagen de temperatuur langzaam laten stijgen tot 10 ºC. Meet na het vergisten en voor het bottelen de SG weer.";
            beer.StaticValues.BottlingDescription = $"Dit bier moet een CO₂ volume hebben dat tussen de <strong>{beer.StaticValues.MinimumCO2}</strong> en <strong>{beer.StaticValues.MaximumCO2}</strong> ligt. Gebruik <a href=\"https://www.brewersfriend.com/beer-priming-calculator/\" target=\"_blank\">deze calculator</a> om te bepalen hoeveel suiker er nodig is. Na het bottelen twee weken laten nagisten op 19 ºC.";

            Add(beer);
        }
    }
}