"use client";

import { useEffect, useState } from "react";
import { getProducts } from "@/apis/productApis";
import { Product } from "@/types/productTypes";

export default function Products() {
	const [products, setProducts] = useState<Product[]>([]);

	useEffect(() => {
		const fetchProducts = async () => {
			const products = await getProducts();
			setProducts(products);
		}
		fetchProducts();
	}, [setProducts]);

	return (
		<>
			<table>
				<thead>
					<tr>
						<th>Name</th>
						<th>Description</th>
						<th>Price</th>
					</tr>
				</thead>
				<tbody>
					{products.map((product) => (
						<tr key={product.id}>
							<td>{product.name}</td>
							<td>{product?.description || "none"}</td>
							<td>{product.price}</td>
						</tr>
					))}
				</tbody>
			</table>
		</>
	);
}