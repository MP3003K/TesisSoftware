public class ApiResponse<T>
{
  public bool Succeeded { get; set; }
  public string? Message { get; set; }
  public T? Data { get; set; }
  public string? Error { get; set; }
  public int? ErrorLine { get; set; }
  public int? ErrorNumber { get; set; }
}
