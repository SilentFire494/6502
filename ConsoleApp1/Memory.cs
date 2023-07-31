namespace ConsoleApp1
{
    public class Memory
    {
        private readonly byte[] memory;
        private const ushort zeroPageStart = 0x0000; // Start address of Zero Page
        private const ushort zeroPageEnd = 0x00FF;   // End address of Zero Page
        private const ushort stackStart = 0x0100;    // Start address of Stack
        private const ushort stackEnd = 0x01FF;      // End address of Stack

        public Memory()
        {
            // Initialize memory with a size of 64KB (65536 bytes)
            memory = new byte[65536];
        }

        public void Initialize()
        {
            Array.Clear(memory, 0, memory.Length);
        }

        public byte ReadByte(ushort address)
        {
            // Check if the address is in the Zero Page or Stack, and if so, don't allow reads
            if (address <= zeroPageEnd || (address >= stackStart && address <= stackEnd))
            {
                return 0x00; // Return 0x00 for reads to Zero Page and Stack
            }

            return memory[address];
        }

        public void WriteByte(ushort address, byte value)
        {
            // Check if the address is in the Zero Page or Stack, and if so, don't allow writes
            if (address <= zeroPageEnd || (address >= stackStart && address <= stackEnd))
            {
                return; // Ignore writes to Zero Page and Stack
            }

            memory[address] = value;
        }
    }
}