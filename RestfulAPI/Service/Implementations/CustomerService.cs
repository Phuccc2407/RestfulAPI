//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using RestfulAPI.Helper;
//using RestfulAPI.Modal;
//using RestfulAPI.Repos;
//using RestfulAPI.Repos.Models;
//using RestfulAPI.Service.Interfaces;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace RestfulAPI.Service.Implementations
//{
//    public class CustomerService : ICustomerService
//    {
//        private readonly LearndataContext context;
//        private readonly IMapper mapper;
//        private readonly ILogger<CustomerService> logger;

//        public CustomerService(LearndataContext context, IMapper mapper, ILogger<CustomerService> logger)
//        {
//            this.context = context;
//            this.mapper = mapper;
//            this.logger = logger;
//        }

//        public async Task<APIResponse> Create(CustomerModal data)
//        {
//            APIResponse response = new APIResponse();
//            try
//            {
//                logger.LogInformation("Create Begins");
//                User _user = mapper.Map<CustomerModal, User>(data);
//                await context.Users.AddAsync(_user);
//                await context.SaveChangesAsync();
//                response.ResponseCode = 201;
//                response.Result = data.UserId.ToString();
//            }
//            catch(Exception ex)
//            {
//                response.ResponseCode = 400;
//                response.Errormessage = ex.Message;
//                logger.LogError(ex, ex.Message);
//            }
//            return response;
//        }

//        public async Task<List<CustomerModal>> Getall()
//        {
//            List<CustomerModal> _response = new List<CustomerModal>();

//            var _data = await context.Users
//                .Where(u => !u.IsDeleted)
//                .ToListAsync();

//            if (_data != null) {
//                logger.LogInformation("GetAll Begins");
//                _response = mapper.Map<List<User>, List<CustomerModal>>(_data);
//            }

//            return _response;
//        }

//        public async Task<CustomerModal> GetbyGUID(Guid guid)
//        {
//            CustomerModal _response = new CustomerModal();
//            var _data = await context.Users.FindAsync(guid);
//            if (_data != null)
//            {
//                _response = mapper.Map<User, CustomerModal>(_data);
//            }
//            return _response;
//        }

//        public async Task<APIResponse> Remove(Guid guid)
//        {
//            APIResponse response = new APIResponse();
//            //try
//            //{
//            //    var _customer = await context.Users.FirstOrDefaultAsync(u => u.UserId == guid && !u.IsDeleted);
//            //    if (_customer != null)
//            //    {
//            //        _customer.IsDeleted = true;
//            //        await context.SaveChangesAsync();

//            //        response.ResponseCode = 200;
//            //        response.Result = guid.ToString();
//            //    }
//            //    else
//            //    {
//            //        response.ResponseCode = 404;
//            //        response.Errormessage = "Data not found";
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    response.ResponseCode = 400;
//            //    response.Errormessage = ex.Message;
//            //}
//            return response;
//        }

//        public async Task<APIResponse> Update(CustomerModal data, Guid guid)
//        {
//            APIResponse response = new APIResponse();
//            try
//            {
//                var _customer = await context.Users.FindAsync(guid);
//                if (_customer != null)
//                {
//                    mapper.Map(data, _customer);
//                    _customer.UpdatedAt = DateTime.Now;

//                    await context.SaveChangesAsync();
//                    response.ResponseCode = 200;
//                    response.Result = guid.ToString();
//                }
//                else
//                {
//                    response.ResponseCode = 404;
//                    response.Errormessage = "Data not found";
//                }
//            }
//            catch (Exception ex)
//            {
//                response.ResponseCode = 400;
//                response.Errormessage = ex.Message;
//            }
//            return response;
//        }
//    }
//}
