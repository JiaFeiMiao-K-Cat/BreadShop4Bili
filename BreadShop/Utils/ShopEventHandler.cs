using BreadShop4Bili.BreadShop.Events;
using BreadShop4Bili.BreadShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Utils
{
    public static class ShopEventHandler<T> where T : EventBase
    {
        private static Tuple<int, MethodInfo>[] SingleParametersMethods;
        private static Tuple<int, MethodInfo>[] DoubleParametersMethods;
        private static Random Random = new Random();
        static ShopEventHandler()
        {
            MethodInfo[] methodInfos = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] _singleParametersMethods;
            MethodInfo[] _doubleParametersMethods;
            _singleParametersMethods = methodInfos
                .Where(m => m.GetParameters().Length == 1)
                .Where(e => e.GetCustomAttribute<ShopEventAttribute>() != null)
                .OrderBy(e => e.GetCustomAttribute<ShopEventAttribute>().Priority)
                .ThenBy(e => e.GetCustomAttribute<ShopEventAttribute>().Probability)
                .ToArray();
            _doubleParametersMethods = methodInfos
                .Where(m => m.GetParameters().Length == 2)
                .Where(e => e.GetCustomAttribute<ShopEventAttribute>() != null)
                .OrderBy(e => e.GetCustomAttribute<ShopEventAttribute>().Priority)
                .ThenBy(e => e.GetCustomAttribute<ShopEventAttribute>().Probability)
                .ToArray();
            SingleParametersMethods = _singleParametersMethods
                .Select(e => new Tuple<int, MethodInfo>
                    (e.GetCustomAttribute<ShopEventAttribute>().Probability, e))
                .ToArray();
            DoubleParametersMethods = _doubleParametersMethods
                .Select(e => new Tuple<int, MethodInfo>
                    (e.GetCustomAttribute<ShopEventAttribute>().Probability, e))
                .ToArray();
            for (int i = 1; i < SingleParametersMethods.Length; i++)
            {
                SingleParametersMethods[i] = new Tuple<int, MethodInfo>
                    (SingleParametersMethods[i].Item1 + SingleParametersMethods[i - 1].Item1,
                    SingleParametersMethods[i].Item2);
            }
            for (int i = 1; i < DoubleParametersMethods.Length; i++)
            {
                DoubleParametersMethods[i] = new Tuple<int, MethodInfo>
                    (DoubleParametersMethods[i].Item1 + DoubleParametersMethods[i - 1].Item1,
                    DoubleParametersMethods[i].Item2);
            }
        }
        public static string ExcuteSingle(Profile profile)
        {
            int value = Random.Next(10001);
            Debug.WriteLine(value);
            Tuple<int, MethodInfo> method = SingleParametersMethods.FirstOrDefault(e => e.Item1 >= value);
            if (method == null)
            {
                throw new Exception($"Empty Method Array for Value {value}");
            }
            return method.Item2.Invoke(null, new object[] { profile })?.ToString();
        }
        public static string ExcuteDouble(Profile profile, Profile profile2)
        {
            int value = Random.Next(10001);
            Tuple<int, MethodInfo> method = DoubleParametersMethods.LastOrDefault(e => e.Item1 >= value);
            if (method == null)
            {
                throw new Exception($"Empty Method Array for Value {value}");
            }
            return method.Item2.Invoke(null, new object[] { profile, profile2 })?.ToString();
        }
    }

    public class ConstantSize
    {
        public static int size { get; } = 0;
        public virtual int getSize()
        {
            return size;
        }
    }
    public class ConstantSize1 : ConstantSize
    {
        public static new int size { get; } = 1;
        public override int getSize()
        {
            return size;
        }
    }
    public class ConstantSize2 : ConstantSize
    {
        public static new int size { get; } = 2;
        public override int getSize()
        {
            return size;
        }
    }
    public static class ShopEventHandler<T, S> 
        where T : EventBase
        where S : ConstantSize, new()
    {
        private static Tuple<int, MethodInfo>[] methods;
        private static Random Random = new Random();
        private static S size;
        static ShopEventHandler()
        {
            MethodInfo[] methodInfos = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] _methods;
            size = new S();
            _methods = methodInfos
                .Where(m => m.GetParameters().Length == size.getSize())
                .Where(e => e.GetCustomAttribute<ShopEventAttribute>() != null)
                .OrderBy(e => e.GetCustomAttribute<ShopEventAttribute>().Priority)
                .ThenBy(e => e.GetCustomAttribute<ShopEventAttribute>().Probability)
                .ToArray();
            methods = _methods
                .Select(e => new Tuple<int, MethodInfo>
                    (e.GetCustomAttribute<ShopEventAttribute>().Probability, e))
                .ToArray();
            for (int i = 1; i < methods.Length; i++)
            {
                methods[i] = new Tuple<int, MethodInfo>
                    (methods[i].Item1 + methods[i - 1].Item1,
                    methods[i].Item2);
            }
        }
        public static string Excute(params Profile[] profile)
        {
            Console.WriteLine($"{size.getSize()}, {profile?.Length ?? 0}");
            if (size.getSize() != (profile?.Length??0))
            {
                throw new ArgumentException("Too many or too little argument to excute");
            }
            int value = Random.Next(10001);
            Debug.WriteLine(value);
            Tuple<int, MethodInfo> method = methods.FirstOrDefault(e => e.Item1 >= value);
            if (method == null)
            {
                throw new Exception($"Empty Method Array for Value {value}");
            }
            return method.Item2.Invoke(null, profile)?.ToString();
        }
    }
}
