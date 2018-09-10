using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    /// <summary>

    public class ProductServices : IProductServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ProductServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Fetches product details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public BusinessEntities.ProductEntity GetProductById(int productId)
        {
            var product = _unitOfWork.ProductMasterRepository.GetByID(productId);
            if (product != null)
            {
                Mapper.CreateMap<productmaster, ProductEntity>();
                var productModel = Mapper.Map<productmaster, ProductEntity>(product);
                return productModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductMasterRepository.GetAll().ToList();
            if (products.Any())
            {
                Mapper.CreateMap<productmaster, ProductEntity>();
                var productsModel = Mapper.Map<List<productmaster>, List<ProductEntity>>(products);
                return productsModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public int CreateProduct(BusinessEntities.ProductEntity productEntity)
        {
            using (var scope = new TransactionScope())
            {
                var product = new productmaster
                {
                   ProductName = productEntity.ProductName,
                   ProductTaxable=  productEntity.ProductTaxable,
                   ProductType  =productEntity.ProductType,
                   UnitoMesure  =productEntity.UnitoMesure,
                   AlternameUom =productEntity.AlternameUom, 
                   TaxType=productEntity.TaxType,  
                   Quantity=productEntity.Quantity,  
                   Discount=productEntity.Discount,  
                   PurchasePrice=productEntity.PurchasePrice,  
                   SalePrice=productEntity.SalePrice,  
                   Description=productEntity.Description,  
                   Hscno=productEntity.Hscno  
                 };
                _unitOfWork.ProductMasterRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
               return product.ProductId;
            }
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productEntity"></param>
        /// <returns></returns>
        public bool UpdateProduct(int productId, BusinessEntities.ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductMasterRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.ProductName = productEntity.ProductName.Trim();
                        product.ProductTaxable = productEntity.ProductTaxable.Trim();
                        product.ProductType = productEntity.ProductType.Trim();
                        product.UnitoMesure = productEntity.UnitoMesure.Trim();
                        product.AlternameUom = productEntity.AlternameUom.Trim();
                        product.TaxType = productEntity.TaxType.Trim();
                        product.Quantity = productEntity.Quantity;
                        product.Discount = productEntity.Discount.Trim();
                        product.PurchasePrice = productEntity.PurchasePrice;
                        product.SalePrice = productEntity.SalePrice;
                        product.Description = productEntity.Description.Trim();
                        product.Hscno = productEntity.Hscno.Trim();
                        _unitOfWork.ProductMasterRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(int productId)
        {
            var success = false;
            if (productId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductMasterRepository.GetByID(productId);
                    if (product != null)
                    {

                        _unitOfWork.ProductMasterRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
