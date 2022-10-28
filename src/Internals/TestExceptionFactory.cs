//namespace Applinate.Test
//{
//    //using Applinate.Cache;

//    internal static class TestExceptionFactory
//    {
    
//        public static Exception CacheDependencyNotRegisteredForTest<T>()
//        {
//            return new InvalidOperationException(
//                @$"no value was set for the mock cache for {nameof(ICache<T>)}<{typeof(T).Name}>
//which is a dependency for this test.

//Be sure to call the following cache registration code in the test method:

//Example 1: just register with no result
//--------------------------------------------------------------------------
//{nameof(TestHelper)}.{nameof(TestHelper.SetCacheResultForTestDuration)}<{typeof(T).Name}>(cacheKey => null); 
//--------------------------------------------------------------------------

//example 2: return explicit result
//--------------------------------------------------------------------------
//{nameof(TestHelper)}.{nameof(TestHelper.SetCacheResultForTestDuration)}<{typeof(T).Name}>(cacheKey => 
//{{
//     // prep value or get based on the cacheKey
//    var result = new {typeof(T).Name}();
//    return result;
    
//}})
//");
//        }
//        public static Exception CacheFactoryIncorrectlyCoded<T>()
//        {
//            return new InvalidOperationException(@$"
//The {nameof(MockCacheFactory)} is broken.. the value entered into the dictionary 
//is not the expected type: {typeof(ICache<T>)}");
//        }
//    }
//}
