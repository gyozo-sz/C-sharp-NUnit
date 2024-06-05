namespace NUnit_practice.DataTypes
{
    public class UserQueryResponse : GetRequestResponse<User> { }
    public class ColorQueryResponse : GetRequestResponse<ColorInfo> { }
    public class UserListQueryResponse : ResourceList<User> { }
    public class ColorListQueryResponse : ResourceList<ColorInfo> { }
}
