namespace PosterApi.DTO;

public record PosterDTO
{
	public int Id { get; init; }
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required string ImageUrl { get; init; }	
}