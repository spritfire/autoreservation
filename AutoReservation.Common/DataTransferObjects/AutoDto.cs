namespace AutoReservation.Common.DataTransferObjects
{
    public class AutoDto
    {

        public int Basistarif { get; set; }
        public int Id { get; set; }
        public string Marke { get; set; }
        public byte[] RowVersion { get; set; }
        public int Tagestarif { get; set; }
        public AutoKlasse AutoKlasse { get; set; }

        public override string ToString()
            => $"ID: {Id} | {Marke} ({AutoKlasse}) | Daily cost: {Tagestarif} , Basic cost: {Basistarif}";
    }
}
