const url = process.env.NEXT_PUBLIC_API_URL;

export const getProducts = async () => {
	const response = await fetch(`${url}/api/products`);
	return await response.json();
};