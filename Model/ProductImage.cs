namespace Dot_Net_ECommerceWeb.Model;

public class ProductImage
{
    public int Id { get; set; }
    public string ImgMain { get; set; }
    public string Img1 { get; set; }
    public string Img2 { get; set; }
    public string Img3 { get; set; }
    public string Img4 { get; set; }
    
    // Navigation Property for Product
    public Product Product { get; set; }
}