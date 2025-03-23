using Vizvezetek.API.DTOs;

namespace Vizvezetek.API.DTOs
{
    public class MunkalapDto
    {
        public int id { get; set; }
        public DateTime beadas_datum { get; set; }
        public DateTime javitas_datum { get; set; }
        public string helyszin { get; set; } // Település + utca egy mezőben
        public string szerelo { get; set; } // Szerelő neve egy mezőben
        public int munkaora { get; set; }
        public int anyagar { get; set; }

        public MunkalapDto(int id, DateTime beadas_datum, DateTime javitas_datum, string helyszin, string szerelo, int munkaora, int anyagar)
        {
            this.id = id;
            this.beadas_datum = beadas_datum;
            this.javitas_datum = javitas_datum;
            this.helyszin = helyszin;
            this.szerelo = szerelo;
            this.munkaora = munkaora;
            this.anyagar = anyagar;
        }

        public MunkalapDto(object id, object beadas_datum, object javitas_datum, string v, object nev, object munkaora, object anyagar)
        {
        }
    }
}
