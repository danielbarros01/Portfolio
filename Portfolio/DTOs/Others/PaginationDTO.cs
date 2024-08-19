namespace Portfolio.DTOs.Others
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        private int recordsPerPage = 10;
        private readonly int maxRecordsPerPage = 30;

        public int RecordsPerPage
        {
            get => recordsPerPage;
            set
            {
                recordsPerPage = 
                    value > maxRecordsPerPage ? maxRecordsPerPage : value;
            }
        }
    }
}
