using MicroFx.Mapper;
using System;
using Xunit;
using System.Linq.Expressions;

namespace TestMapper
{
    public class MapperTest
    {
        [Fact]
        public void Test1()
        {
            var order = new Order();

            var map = Mapper.CreateMap<Order, OrderDto>();
            var map1 = Mapper.CreateMap<OrderDto, Order>();
            order.Title = "test";
            order.Id = 1;
            var orderDto = map.Map(order);
            order = map1.Map(orderDto);
            Assert.Equal(order.Title,orderDto.Title);
            Assert.True(order.Id==orderDto.Id);
        }
    }

    class Order
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }

    class OrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

    }
}
