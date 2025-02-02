//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 12.13
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

namespace Microsoft.BingAds.V13.Internal.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using V13.Reporting;

    public class RowReportRecord : IReportRecord
    {
        private readonly RowValues rowValues;
    
        public RowReportRecord(RowValues rowValues)
        {
            this.rowValues = rowValues;
        }

        public double GetDoubleValue(string header)
        {

            if (this.rowValues.TryGetValue(header, out var value))
            {
                value = value.Trim();
                if (value == string.Empty || value.Equals("--")) return 0.0;
                if (value.EndsWith("%"))
                {
                    value = value.Replace("%", "");
                    double result = double.Parse(value, new CultureInfo("en-US")) / 100.0;
                    return result;
                }
                else
                {
                    double result = double.Parse(value, new CultureInfo("en-US"));
                    return result;
                }
            }

            throw new InvalidColumnException(header);

        }

        public long GetLongValue(string header)
        {

            if (this.rowValues.TryGetValue(header, out string value))
            {
                if (value == String.Empty || value.Equals("--")) return 0L;
                long result = long.Parse(value.Trim());
                return result;
            }

            throw new InvalidColumnException(header);
        }

        public int GetIntegerValue(string header)
        {
            if (rowValues.TryGetValue(header, out string value))
            {
                if (value == String.Empty || value.Equals("--")) return 0;
                int result = int.Parse(value.Trim());
                return result;
            }

            throw new InvalidColumnException(header);
        }

        public string GetStringValue(string header)
        {
            // the rowValues store string value itself, no need to cache
            if (rowValues.TryGetValue(header, out string value))
            {
                return value;
            }

            throw new InvalidColumnException(header);
        }
    }
}
