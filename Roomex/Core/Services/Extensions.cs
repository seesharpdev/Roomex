﻿namespace Roomex.Core.Services
{
	using System;

	public static class NumericExtensions
	{
		public static double ToRadians(this double val)
		{
			return (Math.PI / 180) * val;
		}
	}
}
