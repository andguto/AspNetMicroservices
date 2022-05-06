using MediatR;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrdersListQuery : IRequest<List<OrderViewModel>>
    {
        public string UserName { get; set; }
        public GetOrdersListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

    }
}
