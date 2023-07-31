using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    public class CPUTests
    {
        [Test]
        public void TestLoadAccumulatorImmediate()
        {
            // Arrange
            CPU cpu = new();

            // Load an immediate value (42) into the accumulator
            byte loadImmediateOpcode = 0xA9;
            byte immediateValue = 0x2A; // 42 in decimal
            cpu.WriteByte(0xFFFC, loadImmediateOpcode);
            cpu.WriteByte(0xFFFD, immediateValue);

            // Act
            cpu.Execute(2);

            // Assert
            Assert.AreEqual(immediateValue, cpu.Accumulator, "Accumulator should be loaded with the immediate value.");
        }
    }
}
