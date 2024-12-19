import type { NextConfig } from "next";

const nextConfig: NextConfig = {
	/* config options here */
	// serverRuntimeConfig: {
	// 	Proxy: {
	// 		"/API": {
	// 			target: process.env.NEXT_PUBLIC_API_URL,
	// 			changeOrigin: true,
	// 			pathRewrite: { "^/API": "" }
	// 		}
	// 	}
	// }
};

export default nextConfig;
