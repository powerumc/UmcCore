using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Testing.UnitTest
{
	/// <summary>
	/// 각 테스트에 대한 Build Verification Test 의 상수 클래스 입니다.
	/// </summary>
	public static class BVT
	{
		/// <summary>
		/// 일반적인 단위 기능에 대한 검증입니다.
		/// </summary>
		public static string BVT_FUNCTION = "BVT Function";

		/// <summary>
		/// 데이터베이스 연결이 필요한 기능에 대한 검증입니다.
		/// </summary>
		public static string BVT_DATABASE = "BVT Database";

		/// <summary>
		/// 원격 서버 또는 다른 프로세스간의 통신이 필요한 기능에 대한 검증입니다.
		/// </summary>
		public static string BVT_COMMUNICATION = "BVT Communication";

		/// <summary>
		/// 클라이언트 제품에 대한 기능 검증입니다.
		/// </summary>
		public static string BVT_PRODUCT_CLIENT = "BVT Client";

		/// <summary>
		/// 서버 제품에 대한 기능 검증입니다.
		/// </summary>
		public static string BVT_PRODUCT_SERVER = "BVT Server";
	}
}
