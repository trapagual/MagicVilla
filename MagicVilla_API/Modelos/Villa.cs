namespace MagicVilla_API.Modelos
{
    public class Villa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Villa(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = Nombre;
        }
    }
}
