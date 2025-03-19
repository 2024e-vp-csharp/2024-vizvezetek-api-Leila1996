using Vizvezetek.API.DTOs;

namespace Vizvezetek.API.DTOs
{
    public class MunkalapDto
    {
        public int Id { get; set; }
        public DateTime BeadasDatum { get; set; }
        public DateTime JavitasDatum { get; set; }
        public string Helyszin { get; set; }
        public string Szerelo { get; set; }
        public int Munkaora { get; set; }
        public int Anyagar { get; set; }

        // Konstruktor, amely tartalmazza az id-t és a többi mezőt
        public MunkalapDto(int id, DateTime beadasDatum, DateTime javitasDatum, string telepules, string utca, string szerelo, int munkaora, int anyagar)
        {
            Id = id;
            BeadasDatum = beadasDatum;
            JavitasDatum = javitasDatum;
            Helyszin = $"{telepules}, {utca}";
            Szerelo = szerelo;
            Munkaora = munkaora;
            Anyagar = anyagar;
        }

        public MunkalapDto(object id, object beadas_datum, object javitas_datum, string v, object nev, object munkaora, object anyagar)
        {
        }
    }
}
