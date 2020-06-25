using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PayT.Application.Dtos;
using PayT.Application.ReadModels;
using PayT.Domain.Entities;
using PayT.Infrastructure.EventStore;
using PayT.Infrastructure.Repositories;
using PayT.Infrastructure.Types;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using Xunit;

namespace PayT.Web.Tests
{
    public class SubjectControllerTests
    {
        public class BasicTests
            : IClassFixture<PayTWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;

            public BasicTests(PayTWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
            }

            [Fact]
            public async Task Creating_And_Paying_Subject_Should_Work()
            {
                var threshold = 5000;
                var url = "/api/subjects";
                var subjectDto = new SubjectForCreationDto() {Id = Guid.NewGuid().ToString(), Name = "sample", Amount = 5};
                var billDto = new BillForCreationDto() {Amount = 5};
                _client.PostAsync(url,
                    new StringContent(JsonConvert.SerializeObject(subjectDto), Encoding.UTF8, "application/json")).Wait();
                _client.PutAsync($"{url}/{subjectDto.Id}/bills",
                    new StringContent(JsonConvert.SerializeObject(billDto), Encoding.UTF8, "application/json")).Wait();

                Thread.Sleep(threshold);
                var result = await _client.GetAsync(url);

                var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectReadModel>>(await result.Content.ReadAsStringAsync());

                var subject = subjects.FirstOrDefault(s => s.Id == new Guid(subjectDto.Id));

                Assert.Equal("sample", subject.Name);
                Assert.Equal(5, subject.Amount);
                Assert.Equal(5, subject.Bills.First().Amount);
            }
        }
    }
}