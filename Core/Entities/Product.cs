using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        
        //Так як у нас зв'язок багато до одного з брендами, то ми можемо лише вказати foreign key тут
        //І в ProductBrand нічого не треба робити, бо ми можемо доступитися через Product|...|ProductBrand
        //                                                                        1           3
        //                                                                        2           4
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}