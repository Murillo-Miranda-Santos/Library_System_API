namespace LibrarySystemAPI.DTOs.loans;

public class LoanResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string BookTitle { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; }
}
