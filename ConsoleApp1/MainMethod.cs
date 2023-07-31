namespace ConsoleApp1.Tests
{
    public class MainMethod
    {
        public static void Main(string[] args)
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
            System.Console.WriteLine("Accumulator should be loaded with the immediate value.");
            System.Console.WriteLine("Accumulator: " + cpu.Accumulator);
            System.Console.WriteLine("Immediate Value: " + immediateValue);
            System.Console.WriteLine("Accumulator == Immediate Value: " + (cpu.Accumulator == immediateValue));

        }
    }
}