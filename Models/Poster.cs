using PosterApi.Interfaces;

namespace PosterApi.Models;
public class Poster : AuditEntity
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public required string ImageUrl { get; set; }	
}