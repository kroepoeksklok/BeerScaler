using BeerScaler.Dtos;

namespace BeerScaler.Data {
    public static class Malts {
        public static Ingredient BarkeMunich => new Ingredient("Barke Munich", MaltingCompanies.Weyermann);
        public static Ingredient CaramunichTypeII => new Ingredient("CaraMunich Type II", MaltingCompanies.Weyermann);
        public static Ingredient MildAleMalt => new Ingredient("Mild Ale Malt", MaltingCompanies.ThomasFawcett);
        public static Ingredient CaraAroma => new Ingredient("CaraAroma", MaltingCompanies.Weyermann);
        public static Ingredient PilsenMd => new Ingredient("Pilsen MD", MaltingCompanies.Dingemans);
        public static Ingredient WheatMalt => new Ingredient("Tarwemout", MaltingCompanies.Brewferm);
        public static Ingredient PaleAleMalt => new Ingredient("Pale ale malt", MaltingCompanies.Dingemans);
        public static Ingredient CaraHell => new Ingredient("CaraHell", MaltingCompanies.Weyermann);
    }
}