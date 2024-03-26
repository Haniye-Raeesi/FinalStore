using _0_FrameWork.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var Operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            { return Operation.Failed(ApplicationMessages.Duplicated); }
            else
            {
                var StartDate = command.StartDate.ToGeorgianDateTime();
                var EndDate = command.EndDate.ToGeorgianDateTime();
                var CUstomerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate,
                    command.Reason, StartDate, EndDate);
                _customerDiscountRepository.Create(CUstomerDiscount);
                _customerDiscountRepository.Save();
                return Operation.Successful();

            }
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
           var operationResult = new OperationResult();
           var CustomerDisc = _customerDiscountRepository.Get(command.Id);
            if (CustomerDisc==null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId
            && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.Duplicated);

            var StartDate = command.StartDate.ToGeorgianDateTime();
            var EndDate = command.EndDate.ToGeorgianDateTime();
            CustomerDisc.Edit(command.ProductId,command.DiscountRate,command.Reason,StartDate,EndDate);
            _customerDiscountRepository.Save();
            return operationResult.Successful();
        }

        public EditCustomerDiscount GetDetails(long Id)
        {
            return _customerDiscountRepository.GetDetails(Id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }

    }
}
