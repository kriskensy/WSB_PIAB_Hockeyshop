namespace Hockeyshop.Intranet.Models
{
    public class CrudIndexContainerModel
    {
        public string Title { get; set; }
        public string CreateAction { get; set; } = "Create"; //akcja kontrolera dla buttona Create New
        public bool EnableSearch { get; set; } = false; //flaga na wyświetlanie pola wyszukiwania
        public string SearchTerm { get; set; } //przechowuje wyszukiwanie
        public List<string> Columns { get; set; }
        public IEnumerable<object> Rows { get; set; } //kolekcja wierszy jako objekt bo różne modele
        public Func<object, string, object> GetValue { get; set; } //ta funkcja zwraca wartość dla danego wiersza i danej kolumny
        public Func<object, string> ActionLinks { get; set; } //ta funkcja generuje buttony akcji dla rekordu
    }
}
