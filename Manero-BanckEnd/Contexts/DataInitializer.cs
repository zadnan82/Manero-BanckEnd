using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Manero_BanckEnd.Contexts
{
    public class DataInitializer
    {
        private readonly DataContext _dbContext;

        public DataInitializer(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }

        private void SeedData()
        {
            
            if (!_dbContext.Products
            .Any(e => e.Name == "T-Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "TS-01",
                    Name = "T-Shirt",
                    Category="BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Beige" } },
                    Price = 150,
                    Description = "A comfortable T-shirt made of organic cotton.",
                    Quantity = 100,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false,
                    ImageLink = "https://cdn.pixabay.com/photo/2016/03/25/09/04/t-shirt-1278404_1280.jpg"

                });
            }
            if (!_dbContext.Products
            .Any(e => e.Name == "Jeans"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "JN-01",
                    Name = "Jeans",
                    Category="Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "30" }, new SizeEntity { Size = "32" }, new SizeEntity { Size = "34" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Grey" }, new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "Black" } },
                    Price = 349,
                    Description = "Classic denim jeans.",
                    Quantity = 50,
                    ProductType = ProductType.Pants,
                    IsFeatured = true,
                    ImageLink = "https://cdn.pixabay.com/photo/2018/10/10/14/25/pants-3737416_1280.jpg"
                });
            }
            if (!_dbContext.Products
            .Any(e => e.Name == "Dress Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "DS-01",
                    Name = "Dress Shirt",
                    Category="BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Red" }, new ColorEntity { Color = "Light blue" }, new ColorEntity { Color = "Black" } },
                    Price = 200,
                    Description = "A formal dress shirt.",
                    Quantity = 75,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false,
                    ImageLink = "https://cdn.pixabay.com/photo/2017/06/20/22/57/women-2425268_1280.jpg"
                });
            }
            if (!_dbContext.Products.Any(e => e.Name == "Sweatshirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SW-01",
                    Name = "Sweatshirt",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" }, new SizeEntity { Size = "XL" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Gray" }, new ColorEntity { Color = "Navy" }, new ColorEntity { Color = "Black" } },
                    Price = 180,
                    Description = "A warm and comfortable sweatshirt.",
                    Quantity = 60,
                    ProductType = ProductType.Shirts,
                    IsFeatured = true,
                    ImageLink = "https://cdn.pixabay.com/photo/2019/11/22/05/56/smiling-4644153_1280.jpg"
                });
            }

            // Product 5
            if (!_dbContext.Products.Any(e => e.Name == "Sneakers"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SN-01",
                    Name = "Sneakers",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "38" }, new SizeEntity { Size = "39" }, new SizeEntity { Size = "40" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "White" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Red" } },
                    Price = 120,
                    Description = "Stylish and comfortable sneakers.",
                    Quantity = 80,
                    ProductType = ProductType.Shoes,
                    IsFeatured = true,
                    ImageLink = "https://cdn.pixabay.com/photo/2023/05/29/13/10/shoes-8026038_1280.jpg"
                });
            }

            // Product 6
            if (!_dbContext.Products.Any(e => e.Name == "Backpack"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "BP-01",
                    Name = "Backpack",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "Green" } },
                    Price = 50,
                    Description = "A durable and spacious backpack.",
                    Quantity = 120,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://cdn.pixabay.com/photo/2016/11/18/19/39/backpack-1836594_1280.jpg"
                });
            }

            // Continue adding products...

            // Product 7
            if (!_dbContext.Products.Any(e => e.Name == "Leather Jacket"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "LJ-01",
                    Name = "Leather Jacket",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" }, new SizeEntity { Size = "XL" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Red" } },
                    Price = 300,
                    Description = "A stylish leather jacket for a bold look.",
                    Quantity = 40,
                    ProductType = ProductType.Shirts,
                    IsFeatured = true,
                    ImageLink = "https://cdn.pixabay.com/photo/2021/03/27/20/11/fashion-6129540_1280.jpg"
                });
            }

            // Product 8
            if (!_dbContext.Products.Any(e => e.Name == "Winter Hat"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "WH-01",
                    Name = "Winter Hat",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "Red" }, new ColorEntity { Color = "White" } },
                    Price = 25,
                    Description = "Keep warm with this stylish winter hat.",
                    Quantity = 90,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://cdn.pixabay.com/photo/2016/11/21/16/11/beanie-1846189_1280.jpg"
                });
            }

            // Product 9
            if (!_dbContext.Products.Any(e => e.Name == "Smartwatch"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SW-02",
                    Name = "Smartwatch",
                    Category = " Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Silver" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Gold" } },
                    Price = 150,
                    Description = "Stay connected with this sleek smartwatch.",
                    Quantity = 70,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://cdn.pixabay.com/photo/2017/12/04/06/00/watch-2996385_1280.jpg"
                });
            }
            if (!_dbContext.Products.Any(e => e.Name == "Striped Polo Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SPS-01",
                    Name = "Striped Polo Shirt",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Red" } },
                    Price = 75,
                    Description = "A casual and stylish striped polo shirt.",
                    Quantity = 90,
                    ProductType = ProductType.Shirts,
                    IsFeatured = true,
                    ImageLink = "https://images.unsplash.com/photo-1517231305676-5702be37e853?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 11
            if (!_dbContext.Products.Any(e => e.Name == "Cargo Pants"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CP-01",
                    Name = "Cargo Pants",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "30" }, new SizeEntity { Size = "32" }, new SizeEntity { Size = "34" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Khaki" }, new ColorEntity { Color = "Olive" }, new ColorEntity { Color = "Black" } },
                    Price = 90,
                    Description = "Comfortable cargo pants with multiple pockets.",
                    Quantity = 65,
                    ProductType = ProductType.Pants,
                    IsFeatured = false,
                    ImageLink = "https://images.unsplash.com/photo-1649850874075-49e014357b9d?q=80&w=1915&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 12
            if (!_dbContext.Products.Any(e => e.Name == "Leather Belt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "LB-01",
                    Name = "Leather Belt",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Tan" } },
                    Price = 25,
                    Description = "A classic leather belt for a polished look.",
                    Quantity = 120,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://images.unsplash.com/photo-1624222247344-550fb60583dc?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 13
            if (!_dbContext.Products.Any(e => e.Name == "Running Shoes"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "RS-01",
                    Name = "Running Shoes",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "42" }, new SizeEntity { Size = "43" }, new SizeEntity { Size = "" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "Gray" }, new ColorEntity { Color = "White" } },
                    Price = 120,
                    Description = "Lightweight and comfortable running shoes.",
                    Quantity = 80,
                    ProductType = ProductType.Shoes,
                    IsFeatured = false,
                    ImageLink = "https://cdn.example.com/running-shoes.jpg"
                });
            }
            if (!_dbContext.Products.Any(e => e.Name == "Floral Print Dress"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "FPD-01",
                    Name = "Floral Print Dress",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Pink" }, new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "Green" } },
                    Price = 120,
                    Description = "A beautiful floral print dress for any occasion.",
                    Quantity = 70,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false,
                    ImageLink = "https://images.unsplash.com/photo-1517556140558-03455e159651?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 23
            if (!_dbContext.Products.Any(e => e.Name == "Slim Fit Jeans"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SFJ-01",
                    Name = "Slim Fit Jeans",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "28" }, new SizeEntity { Size = "30" }, new SizeEntity { Size = "32" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Dark Blue" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Gray" } },
                    Price = 89,
                    Description = "Modern slim fit jeans for a stylish look.",
                    Quantity = 60,
                    ProductType = ProductType.Pants,
                    IsFeatured = true,
                    ImageLink = "https://images.unsplash.com/photo-1472749551955-e2319841d9a0?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 24
            if (!_dbContext.Products.Any(e => e.Name == "Canvas Backpack"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CBP-01",
                    Name = "Canvas Backpack",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Olive" }, new ColorEntity { Color = "Navy" } },
                    Price = 55,
                    Description = "Stylish and durable canvas backpack for everyday use.",
                    Quantity = 100,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://images.unsplash.com/photo-1655303219938-3a771279c801?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 25
            if (!_dbContext.Products.Any(e => e.Name == "Casual Slip-On Shoes"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CSOS-01",
                    Name = "Casual Slip-On Shoes",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "42" }, new SizeEntity { Size = "43" }, new SizeEntity { Size = "44" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Tan" }, new ColorEntity { Color = "Gray" } },
                    Price = 65,
                    Description = "Comfortable slip-on shoes for a casual look.",
                    Quantity = 85,
                    ProductType = ProductType.Shoes,
                    IsFeatured = true,
                    ImageLink = "https://images.unsplash.com/photo-1528701800487-ba01fea498c0?q=80&w=1740&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }


            // Product 32
            if (!_dbContext.Products.Any(e => e.Name == "Classic White Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CWS-01",
                    Name = "Classic White Shirt",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "White" } },
                    Price = 80,
                    Description = "A timeless classic white shirt for any occasion.",
                    Quantity = 90,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false,
                    ImageLink = "https://images.unsplash.com/photo-1690179738473-85845774ed89?q=80&w=1827&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                });
            }

            // Product 33
            if (!_dbContext.Products.Any(e => e.Name == "Khaki Chinos"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "KC-01",
                    Name = "Khaki Chinos",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "30" }, new SizeEntity { Size = "32" }, new SizeEntity { Size = "34" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Khaki" } },
                    Price = 75,
                    Description = "Classic khaki chinos for a smart casual look.",
                    Quantity = 70,
                    ProductType = ProductType.Pants,
                    IsFeatured = true,
                    ImageLink = "https://www.realmenrealstyle.com/wp-content/uploads/2014/10/chinos.jpg"
                });
            }

            // Product 34
            if (!_dbContext.Products.Any(e => e.Name == "Leather Wallet"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "LW-01",
                    Name = "Leather Wallet",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Navy" } },
                    Price = 30,
                    Description = "A sleek leather wallet for your essentials.",
                    Quantity = 110,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/25/0c/be/250cbedb197e6fc59fce405667304eca.jpg"
                });
            }

            // Product 35
            if (!_dbContext.Products.Any(e => e.Name == "High-Top Sneakers"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "HTS-01",
                    Name = "High-Top Sneakers",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "38" }, new SizeEntity { Size = "39" }, new SizeEntity { Size = "40" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Red" } },
                    Price = 95,
                    Description = "Fashionable high-top sneakers for a trendy look.",
                    Quantity = 75,
                    ProductType = ProductType.Shoes,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/dc/89/26/dc8926c48eb9a86492ee51e0953c793a.jpg"
                });
            }

            // Product 38
            if (!_dbContext.Products.Any(e => e.Name == "Trendy Sunglasses"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "TS-03",
                    Name = "Trendy Sunglasses",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Blue" }, new ColorEntity { Color = "Brown" } },
                    Price = 35,
                    Description = "Stay stylish with these trendy sunglasses.",
                    Quantity = 95,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/0b/a7/3a/0ba73acf3a454c8b746f7d1931b4d746.jpg"
                });
            }

            // Product 39
            if (!_dbContext.Products.Any(e => e.Name == "Running Shoes"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "RS-02",
                    Name = "Running Shoes",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "44" }, new SizeEntity { Size = "45" }, new SizeEntity { Size = "46" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Gray/Blue" }, new ColorEntity { Color = "Black/Red" }, new ColorEntity { Color = "White/Silver" } },
                    Price = 80,
                    Description = "Comfortable running shoes for your active lifestyle.",
                    Quantity = 60,
                    ProductType = ProductType.Shoes,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/54/81/dd/5481dd5c8cd4e767c406a8357fb9560a.jpg"
                });
            }


            if (!_dbContext.Products.Any(e => e.Name == "Printed Tote Bag"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "PTB-01",
                    Name = "Printed Tote Bag",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Floral" }, new ColorEntity { Color = "Striped" }, new ColorEntity { Color = "Geometric" } },
                    Price = 40,
                    Description = "A stylish printed tote bag for everyday use.",
                    Quantity = 75,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/bd/1d/d3/bd1dd39d12d2ab543758ccbd2fac88c8.jpg"
                });
            }

            // Product 47
            if (!_dbContext.Products.Any(e => e.Name == "Loafer Shoes"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "LS-01",
                    Name = "Loafer Shoes",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "43" }, new SizeEntity { Size = "44" }, new SizeEntity { Size = "45" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Tan" } },
                    Price = 65,
                    Description = "Classic and comfortable loafer shoes.",
                    Quantity = 85,
                    ProductType = ProductType.Shoes,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/3e/48/b0/3e48b0d204cb213634e642521eacb9b1.jpg"
                });
            }

            // Product 48
            if (!_dbContext.Products.Any(e => e.Name == "Stainless Steel Watch"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SSW-01",
                    Name = "Stainless Steel Watch",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Silver" }, new ColorEntity { Color = "Gold" }, new ColorEntity { Color = "Rose Gold" } },
                    Price = 95,
                    Description = "Elegant stainless steel watch for a polished look.",
                    Quantity = 70,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/9b/07/db/9b07dbdc48ee4d6792690835651b3283.jpg"
                });
            }

            // Product 49
            if (!_dbContext.Products.Any(e => e.Name == "Hiking Boots"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "HB-01",
                    Name = "Hiking Boots",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "42" }, new SizeEntity { Size = "43" }, new SizeEntity { Size = "44" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Green" }, new ColorEntity { Color = "Black" } },
                    Price = 110,
                    Description = "Durable hiking boots for your outdoor adventures.",
                    Quantity = 55,
                    ProductType = ProductType.Shoes,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/80/15/c9/8015c9f5bccc79d9e0cd48a16c8f4711.jpg"
                });
            }
            // Product 51
            if (!_dbContext.Products.Any(e => e.Name == "Graphic T-Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "GT-01",
                    Name = "Graphic T-Shirt",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Gray" } },
                    Price = 25,
                    Description = "Express yourself with a stylish graphic t-shirt.",
                    Quantity = 120,
                    ProductType = ProductType.Shirts,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/736x/3b/b7/72/3bb772ce0a1ad392ef12e3a35366beae.jpg"
                });
            }

            // Product 52
            if (!_dbContext.Products.Any(e => e.Name == "Straight Jeans"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SJ-01",
                    Name = "Straight Jeans",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "28" }, new SizeEntity { Size = "30" }, new SizeEntity { Size = "32" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Dark Blue" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Charcoal" } },
                    Price = 55,
                    Description = "Modern and stylish straight jeans for a sleek look.",
                    Quantity = 90,
                    ProductType = ProductType.Pants,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/cb/e9/23/cbe923a2d7e798157cd64fad7fb19af6.jpg"
                });
            }

            // Product 53
            if (!_dbContext.Products.Any(e => e.Name == "Classic Fedora Hat"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CFH-01",
                    Name = "Classic Fedora Hat",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Gray" } },
                    Price = 30,
                    Description = "Add a touch of sophistication with a classic fedora hat.",
                    Quantity = 80,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/55/93/e0/5593e0482b8aeb11782d11cafdeff2ec.jpg"
                });
            }

            // Product 54
            if (!_dbContext.Products.Any(e => e.Name == "Canvas Sneakers"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CS-01",
                    Name = "Canvas Sneakers",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "40" }, new SizeEntity { Size = "41" }, new SizeEntity { Size = "42" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "White" }, new ColorEntity { Color = "Navy" }, new ColorEntity { Color = "Red" } },
                    Price = 40,
                    Description = "Casual and versatile canvas sneakers for everyday wear.",
                    Quantity = 100,
                    ProductType = ProductType.Shoes,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/29/b6/8d/29b68d27cb07fe29846f48fe931ab641.jpg"
                });
            }

            // Product 55
            if (!_dbContext.Products.Any(e => e.Name == "Printed Scarf"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "PS-01",
                    Name = "Printed Scarf",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Pink Floral" }, new ColorEntity { Color = "Blue Geometric" }, new ColorEntity { Color = "Green Striped" } },
                    Price = 18,
                    Description = "Accessorize with a trendy printed scarf.",
                    Quantity = 150,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/b1/90/6a/b1906a1fe7f0d79035b154a932427d7b.jpg"
                });
            }

            // Product 56
            if (!_dbContext.Products.Any(e => e.Name == "High-Waisted Shorts"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "HWS-01",
                    Name = "High-Waisted Shorts",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Khaki" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Denim Blue" } },
                    Price = 32,
                    Description = "Stay on trend with stylish high-waisted shorts.",
                    Quantity = 70,
                    ProductType = ProductType.Pants,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/59/8a/cb/598acbb580bc891fd8866be3f5f6405c.jpg"
                });
            }

            // Product 57
            if (!_dbContext.Products.Any(e => e.Name == "Leather Crossbody Bag"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "LCB-01",
                    Name = "Leather Crossbody Bag",
                    Category = "Featured",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Tan" } },
                    Price = 50,
                    Description = "A stylish and practical leather crossbody bag.",
                    Quantity = 60,
                    ProductType = ProductType.Accessories,
                    IsFeatured = true,
                    ImageLink = "https://i.pinimg.com/564x/c4/5b/79/c45b79ad248a97c9679ffe38a571f450.jpg"
                });
            }
            // Product 74
            if (!_dbContext.Products.Any(e => e.Name == "Floral Print Maxi Dress"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "FPMD-01",
                    Name = "Floral Print Maxi Dress",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Blue Floral" }, new ColorEntity { Color = "Pink Floral" }, new ColorEntity { Color = "Yellow Floral" } },
                    Price = 55,
                    Description = "Make a statement with this elegant floral print maxi dress.",
                    Quantity = 60,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/21/84/43/218443172ef0a10aa983f83ab254e6f7.jpg"
                });
            }

            // Product 75
            if (!_dbContext.Products.Any(e => e.Name == "Slim Fit Chino Pants"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SFCP-01",
                    Name = "Slim Fit Chino Pants",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "30" }, new SizeEntity { Size = "32" }, new SizeEntity { Size = "34" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Olive" }, new ColorEntity { Color = "Navy" }, new ColorEntity { Color = "Khaki" } },
                    Price = 45,
                    Description = "Achieve a polished look with these slim fit chino pants.",
                    Quantity = 50,
                    ProductType = ProductType.Pants,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/96/05/4a/96054a7e5994f0197a093ef8d8f5cb01.jpg"
                });
            }

            // Product 76
            if (!_dbContext.Products.Any(e => e.Name == "Stylish Crossbody Bag"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "SCB-01",
                    Name = "Stylish Crossbody Bag",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Tan" }, new ColorEntity { Color = "Red" } },
                    Price = 35,
                    Description = "Complete your look with this stylish and versatile crossbody bag.",
                    Quantity = 70,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/7d/f4/ab/7df4ab09f04016579f60ba52631aa89e.jpg"
                });
            }

            // Product 77
            if (!_dbContext.Products.Any(e => e.Name == "Classic Aviator Sunglasses"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CAS-01",
                    Name = "Classic Aviator Sunglasses",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "One Size" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Gold Frame" }, new ColorEntity { Color = "Silver Frame" }, new ColorEntity { Color = "Black Frame" } },
                    Price = 22,
                    Description = "Add a timeless touch to your style with classic aviator sunglasses.",
                    Quantity = 80,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/59/7e/3d/597e3de43781a839ae2084ecb432c70c.jpg"
                });
            }

            // Product 78
            if (!_dbContext.Products.Any(e => e.Name == "Comfortable Slip-On Sneakers"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CSS-02",
                    Name = "Comfortable Slip-On Sneakers",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "38" }, new SizeEntity { Size = "39" }, new SizeEntity { Size = "40" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "White" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Gray" } },
                    Price = 32,
                    Description = "Experience comfort and style with these slip-on sneakers.",
                    Quantity = 90,
                    ProductType = ProductType.Shoes,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/86/79/b9/8679b94d6377c3b6cae9c67a3300aa18.jpg"
                });
            }

            // Product 79
            if (!_dbContext.Products.Any(e => e.Name == "Chic Leather Belt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "CLB-01",
                    Name = "Chic Leather Belt",
                    Category = "BestSeller",
                    Size = new List<SizeEntity> { new SizeEntity { Size = "32" }, new SizeEntity { Size = "34" }, new SizeEntity { Size = "36" } },
                    Color = new List<ColorEntity> { new ColorEntity { Color = "Brown" }, new ColorEntity { Color = "Black" }, new ColorEntity { Color = "Tan" } },
                    Price = 18,
                    Description = "Complete your outfit with this chic leather belt.",
                    Quantity = 100,
                    ProductType = ProductType.Accessories,
                    IsFeatured = false,
                    ImageLink = "https://i.pinimg.com/564x/49/55/07/49550783fe40bc5e3f5c428c2530956e.jpg"
                });
            }


        }
    }
}
