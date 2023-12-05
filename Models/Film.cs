namespace MovieAPI
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateOnly DateSortie { get; set; }
        public double NoteGlobale { get; set; }

    }
}
