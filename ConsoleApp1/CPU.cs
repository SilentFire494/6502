namespace ConsoleApp1
{
    public class CPU
    {
        private readonly Memory memory;
        private byte accumulator;
        private byte xRegister;
        private byte yRegister;
        private byte stackPointer;
        private ushort programCounter;
        private byte statusRegister;

        public byte Accumulator { get => accumulator; }
        public byte XRegister { get => xRegister; }
        public byte YRegister { get => yRegister; }
        public byte StackPointer { get => stackPointer; }
        public ushort ProgramCounter { get => programCounter; }
        public byte StatusRegister { get => statusRegister; }

        private readonly byte carryFlag = 0b00000001;
        private readonly byte zeroFlag = 0b00000010;
        private readonly byte interruptDisableFlag = 0b00000100;
        private readonly byte decimalModeFlag = 0b00001000;
        private readonly byte breakCommandFlag = 0b00010000;
        private readonly byte overflowFlag = 0b01000000;
        private readonly byte negativeFlag = 0b10000000;

        private uint clockCycles;

        private Dictionary<byte, Action> opcodeHandlers;
        private Dictionary<byte, byte> opcodeCycleCounts;

        public CPU()
        {
            memory = new Memory();
            opcodeHandlers = new Dictionary<byte, Action>();
            opcodeCycleCounts = new Dictionary<byte, byte>();
            Reset();
            InitializeOpcodeHandlers();
            InitializeOpcodeCycleCounts();
        }

        public void Reset()
        {
            accumulator = 0;
            xRegister = 0;
            yRegister = 0;
            stackPointer = 0xFD;
            programCounter = 0xFFFC;
            statusRegister = 0x00;
            memory.Initialize();
        }

        public void Execute(uint clockCycles)
        {
            this.clockCycles += clockCycles;
            while (this.clockCycles > 0)
            {
                byte opcode = memory.ReadByte(programCounter);
                if (opcodeHandlers.TryGetValue(opcode, out Action opcodeHandler))
                {
                    opcodeHandler();
                }
                else
                {
                    throw new Exception($"Invalid opcode: {opcode}");
                }
            }
        }

        public void WriteByte(ushort address, byte value)
        {
            memory.WriteByte(address, value);
        }

        public void SetFlag(byte flag, bool value)
        {
            if (value)
            {
                statusRegister |= flag;
            }
            else
            {
                statusRegister &= (byte)~flag;
            }
        }

        private void SetZeroAndNegativeFlags(byte value)
        {
            if (value == 0)
            {
                statusRegister |= zeroFlag;
            }
            else
            {
                statusRegister &= (byte)~zeroFlag;
            }

            if ((value & 0b10000000) > 0)
            {
                statusRegister |= negativeFlag;
            }
            else
            {
                statusRegister &= (byte)~negativeFlag;
            }
        }

        private void LoadAccumulatorImmediate()
        {
            accumulator = memory.ReadByte(programCounter);
            SetZeroAndNegativeFlags(accumulator);
            clockCycles -= 2;
            programCounter++;
        }

        private void InitializeOpcodeHandlers()
        {
            opcodeHandlers.Add(0xA9, LoadAccumulatorImmediate);
        }

        private void InitializeOpcodeCycleCounts()
        {
            opcodeCycleCounts.Add(0xA9, 2);
        }
    }
}