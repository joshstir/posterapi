using Microsoft.EntityFrameworkCore;

namespace PosterApi.Models;

public class PosterContext : DbContext
{
	public PosterContext(DbContextOptions<PosterContext> options)
		: base(options)
	{
	}

	public DbSet<Poster> Posters { get; set; }
}
