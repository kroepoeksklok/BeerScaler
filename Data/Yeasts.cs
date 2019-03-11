using BeerScaler.Dtos;

namespace BeerScaler.Data {
    public static class Yeasts {
        public static Ingredient SafaleUs05 => new Ingredient("Safale US-05", "Fermentis");
        public static Ingredient Wyeast3724 => new Ingredient("3724 - Belgian Saison", "Wyeast");
        public static Ingredient Wyeast1388 => new Ingredient("1388 - Belgian Strong Ale", "Wyeast");
        public static Ingredient Wyeast2124 => new Ingredient("2124 - Bohemian Lager", "Wyeast");
    }
}