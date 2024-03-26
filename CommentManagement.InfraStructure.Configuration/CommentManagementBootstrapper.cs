using CommentManagement.Application;
using CommentManagement.Application.Contract;
using CommentManagement.Domain;
using CommentManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace CommentManagement.Infrastructure.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection Services, String ConnectionString) 
        {
            Services.AddTransient<ICommentRepository,CommentRepository>();
            Services.AddTransient<ICommentApplication,CommentApplication>();

            Services.AddDbContext<CommentContext>(
            options => options.UseSqlServer(
            (ConnectionString),
            providerOptions => providerOptions.EnableRetryOnFailure()));
        }
    }
}
