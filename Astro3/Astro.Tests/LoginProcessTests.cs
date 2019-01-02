using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstroLibrary;
using Xunit;

namespace Astro.Tests
{
    public class LoginProcessTests
    {
        //[Fact]
        [Theory]
        [InlineData ("j", "c", true)]
        [InlineData ("jjj", "pp", true)]
        [InlineData ("1", "Password", false)]
        [InlineData ("", "", false)]
        [InlineData ("jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj", "Password", false)]
        [InlineData ("jbloggs", "PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP", false)]
        [InlineData ("-user", "Password", false)]
        public void ValidateUserInput_StringsShouldVerify(string username, string password, bool expected)
        {
            // Arrange -- the values and set up for test
            LoginProcess loginProcess = new LoginProcess();

            // Act -- action performed in the test
            bool actual = loginProcess.ValidateUserInput(username, password);

            // Assert -- what we expect & what output is
            Assert.Equal(expected, actual);


        }

    }
}
