using BeerScaler.Dtos;

namespace BeerScaler.Data {
    public static class Hops {
        public static Ingredient Saaz => new Ingredient("Saaz");
        public static Ingredient Horizon => new Ingredient("Horizon");
        public static Ingredient Cascade => new Ingredient("Cascade");
        public static Ingredient Centennial => new Ingredient("Centennial");
        public static Ingredient HallertauMittelfruh => new Ingredient("Hallertau Mittelfruh");
        public static Ingredient HallertauTradition => new Ingredient("Hallertau Tradition");
        public static Ingredient Tettnanger => new Ingredient("Tettnanger");
        public static Ingredient Perle => new Ingredient("Perle");
    }
}