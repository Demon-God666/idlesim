using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using Godot;
using Godot.Collections;

namespace IdleSim.scenes.shop.components;

public class ItemData
{
	public string ProductName { get; set; }
	public int ProductPrice { get; set; }
	public Texture2D ProductImage { get; set; }
	
	public Category Category { get; set; }
			
	

	public ItemData(string productName, int productPrice, Texture2D productImage, Category category)
	{
		ProductName = productName;
		ProductPrice = productPrice;
		ProductImage = productImage;
		Category = category;
		
	}
}
