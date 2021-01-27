using AutoMapper;
using Pokeshop.Api.Dtos.CartDtos;
using Pokeshop.Entities.Entities;
using Pokeshop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartReadDto>> GetAsync(string userId)
        {
            var shoppingCart = await _unitOfWork.Carts.GetAsync(userId);
            return _mapper.Map<IEnumerable<CartReadDto>>(shoppingCart);
        }

        public async Task<int> AddAsync(CartAddDto model, string userId)
        {
            var addedItem = _mapper.Map<Cart>(model);
            var productPrice = await _unitOfWork.Products.GetPriceAsync(model.ProductId);
            addedItem.Price = model.Quantity * productPrice;
            addedItem.UserId = userId;

            if (await _unitOfWork.Carts.ExistsAsync(model.ProductId, userId))
            {
                return await _unitOfWork.Carts.UpdateAsync(addedItem);
            }
            return await _unitOfWork.Carts.AddAsync(addedItem);
        }

        public async Task<int> UpdateAsync(CartUpdateDto model, string userId)
        {
            var modifiedItem = _mapper.Map<Cart>(model);
            var productPrice = await _unitOfWork.Products.GetPriceAsync(model.ProductId);
            modifiedItem.Price = model.Quantity * productPrice;
            modifiedItem.UserId = userId;

            return await _unitOfWork.Carts.AddAsync(modifiedItem);
        }

        public async Task DeleteAsync(IEnumerable<int> productids, string userId)
        {
            await _unitOfWork.Carts.DeleteAsync(productids, userId);
        }
    }
}
