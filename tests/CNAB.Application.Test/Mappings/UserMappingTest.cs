using CNAB.Application.DTOs.Account;
using CNAB.Application.Mappings;
using CNAB.Application.Test.Common;
using CNAB.Domain.Entities.Account;
using FluentAssertions;
using Mapster;

namespace CNAB.Application.Test.Mappings;

public class UserMappingTest
{
    private readonly TypeAdapterConfig _config;

    public UserMappingTest()
    {
        _config = new TypeAdapterConfig();
        new UserMapping().Register(_config);
    }

    [Fact]
    public void UserToUserDto_Should_Map_Correctly()
    {
        var user = ServiceTestFactory.CreateUser();

        var userDto = user.Adapt<UserDto>(_config);

        userDto.Should().NotBeNull();
        userDto.Email.Should().Be(user.Email);
        userDto.Password.Should().Be(user.Password);
        userDto.ConfirmPassword.Should().Be(user.ConfirmPassword);
    }

    [Fact]
    public void UserDtoToUser_Should_Map_Correctly()
    {
        var userDto = ServiceTestFactory.CreateUserDto();

        var user = userDto.Adapt<User>(_config);

        user.Should().NotBeNull();
        user.Email.Should().Be(userDto.Email);
        user.Password.Should().Be(userDto.Password);
        user.ConfirmPassword.Should().Be(userDto.ConfirmPassword);
    }

    [Fact]
    public void LoginToLoginDto_Should_Map_Correctly()
    {
        var login = ServiceTestFactory.CreateLogin();

        var loginDto = login.Adapt<LoginDto>(_config);

        loginDto.Should().NotBeNull();
        loginDto.Email.Should().Be(login.Email);
        loginDto.Password.Should().Be(login.Password);
    }

    [Fact]
    public void UserTokenToUserTokenDto_Should_Map_Correctly()
    {
        var userToken = ServiceTestFactory.CreateUserToken();

        var userTokenDto = userToken.Adapt<UserTokenDto>(_config);

        userTokenDto.Should().NotBeNull();
        userTokenDto.Token.Should().Be(userToken.Token);
        userTokenDto.Authenticated.Should().Be(userToken.Authenticated);
        userTokenDto.Expiration.Should().Be(userToken.Expiration);
        userTokenDto.Message.Should().Be(userToken.Message);
    }

    [Fact]
    public void Mapping_NullObject_Should_ReturnNull()
    {
        User nullUser = null;

        var userDto = nullUser.Adapt<UserDto>(_config);

        userDto.Should().BeNull();
    }
}
