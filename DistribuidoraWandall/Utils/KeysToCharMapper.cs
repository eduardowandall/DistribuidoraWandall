using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DistribuidoraWandall.Utils
{
    public static class KeysUtils
    {
        public static bool IsKeyNumber(Key key)
        {
            int keyValue = (int)key;
            return (keyValue > 33 && keyValue < 44) || (keyValue > 73 && keyValue < 84);

        }

        public static int KeyToNumber(Key key)
        {
            if (IsKeyNumber(key))
            {
                int keyValue = (int)key;
                switch (keyValue)
                {
                    case 34:
                    case 74:
                        return 0;
                    case 35:
                    case 75:
                        return 1;
                    case 36:
                    case 76:
                        return 2;
                    case 37:
                    case 77:
                        return 3;
                    case 38:
                    case 78:
                        return 4;
                    case 39:
                    case 79:
                        return 5;
                    case 40:
                    case 80:
                        return 6;
                    case 41:
                    case 81:
                        return 7;
                    case 42:
                    case 82:
                        return 8;
                    case 43:
                    case 83:
                        return 9;
                    default:
                        return 0;
                }
            }
            else
            {
                throw new InvalidCastException("Cannot convert key to a number");
            }

        }
    }
}
