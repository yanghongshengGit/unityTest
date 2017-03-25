/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnePF
{
    public enum OptionsVerifyMode
    {
        /**
         * Verify signatures in any store. 
         * <p>
         * By default in Google's IabHelper. Throws exception if key is not available or invalid.
         * To prevent crashes OpenIAB wouldn't connect to OpenStore if no publicKey provided
         */
        VERIFY_EVERYTHING = 0,

        /**
         * Don't verify signatires. To perform verification on server-side
         */
        VERIFY_SKIP = 1,

        /**
         * Verify signatures only if publicKey is available. Otherwise skip verification. 
         * <p>
         * Developer is responsible for verify
         */
        VERIFY_ONLY_KNOWN = 2
    }
}
