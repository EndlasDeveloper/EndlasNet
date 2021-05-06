using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EndlasNet.Web.Controllers;
namespace EndlasNet.IntegrationTest
{
    public class JobsControllerTests
    {
        public Mock<IJobRepo> mock = new Mock<IJobRepo>();

        [Fact]
        public void MachiningToolForJobDetailsTest()
        {
            var id = Guid.NewGuid();
            var machiningToolForJob = new MachiningToolForJob
            {
                MachiningToolForWorkId = id,
                MachiningType = MachiningTypes.None
            };
            /*mock.Setup(x => x.GetRow(id)).Returns(Task.FromResult(machiningToolForJob));
            MachiningToolForWorksController mtw = new MachiningToolForWorksController(mock.Object);
*/
         /*   mockContext.Setup(c => c.
            Assert.Equal(4, calculator.Object.Add(2, 2));*/
        }
    }
}
