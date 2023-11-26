using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Mnt_Visitor_FavouriteMap : NopEntityTypeConfiguration<Mnt_Visitor_Favourite>
    {
        public Mnt_Visitor_FavouriteMap()
        {
            this.ToTable("Mnt_Visitor_Favourite");
            this.HasKey(p => p.Id);
        }
    }
}
