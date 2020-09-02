
namespace Validation
{
    public class Validate
    {
        private const short k_StartPositionTo = 0;
        private const short k_StartPositionFrom = 3;
        private const short k_SubStringLength = 2;




        public static void ParsePositions(string i_StrInput, ref string io_PositionFrom, ref string io_PositionTo)
        {
            io_PositionTo = i_StrInput.Substring(k_StartPositionFrom, k_SubStringLength);
            io_PositionFrom = i_StrInput.Substring(k_StartPositionTo, k_SubStringLength);
        }
    }
}
