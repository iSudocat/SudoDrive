using System;
using System.Runtime.CompilerServices;

namespace Server.Mixin
{
    public static class Kotlinize
    {
        /// <summary>
        /// Calls the specified function <see cref="block">block</see> and returns its result.<br/>
        /// 
        /// For detailed usage information see the documentation for <a href="https://kotlinlang.org/docs/reference/scope-functions.html#let">scope functions</a><br/>
        /// </summary>
        /// <param name="block"></param>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static R Run<R>(Func<R> block)
            => block();
        
        /// <summary>
        /// Calls the specified function <see cref="block">block</see> with <c>this</c> value as its argument and returns its result.<br/>
        /// 
        /// For detailed usage information see the documentation for <a href="https://kotlinlang.org/docs/reference/scope-functions.html#let">scope functions</a><br/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="block"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static R Let<T, R>(this T self, Func<T, R> block)
            => block(self);


        /// <summary>
        /// Calls the specified function <see cref="block">block</see> with <c>this</c> value as its argument and returns <c>this</c> value.<br/>
        ///
        /// For detailed usage information see the documentation for <a href="https://kotlinlang.org/docs/reference/scope-functions.html#let">scope functions</a><br/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="block"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Also<T>(this T self, Action<T> block)
        {
            block(self);
            return self;
        }
    }
}
