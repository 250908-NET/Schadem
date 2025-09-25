using Xunit;


namespace Store.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        int a = 2;
        int b = 3;

        // Act
        int sum = a + b;

        // Assert
        Assert.Equal(5, sum);
    }
}
