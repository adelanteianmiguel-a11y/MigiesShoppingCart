# 🛒 Ian Adelante Shopping Cart System (Part 2)

This is a console-based Shopping Cart System built in C#.
It simulates a simple store where users can browse products, manage their cart, and complete a checkout process with payment validation and receipt generation.

The system also includes stock management, product search, order history, and low-stock alerts.

🚀 Features (Part 2 Update)
🛍️ Product Management
View all available products
Each product includes:
ID
Name
Category
Price
Stock quantity


🔎 Product Search
Search products by name
Case-insensitive search
Displays matching results with full details


🛒 Cart System
Add products to cart
View cart items
Remove specific items
Clear entire cart
Automatically updates stock when items are added or removed


💳 Checkout System
Displays full receipt
Shows:
Receipt number
Date and time
Purchased items
Grand total
Discount (10% if ₱5000 or more)
Final total


💰 Payment Validation
Requires numeric input only
Rejects invalid input
Ensures payment is not less than total
Automatically calculates change


📦 Stock Management
Deducts stock after purchase
Restores stock when items are removed or cart is cleared
Prevents buying out-of-stock items


⚠️ Low Stock Alert
Shows products with stock 5 or below
Helps identify items that need restocking


📜 Order History
Stores completed transactions during program runtime
Displays past receipts with totals


🤖 AI Usage

I used AI as a learning and development assistant throughout this project. It helped me understand programming concepts, debug errors, and improve the structure of my Shopping Cart System.

AI was especially helpful in breaking down complex logic such as:

Stock management and validation
Cart handling (add, remove, clear)
Payment validation and change computation
Receipt generation flow
Program structure and menu system design

When I encountered errors or logical issues, AI helped me understand the cause rather than just giving direct answers. This improved my problem-solving skills and helped me fix issues like array handling, null checks, and incorrect logic flow.

I also used AI to improve code readability and ensure that the program follows a clean and structured flow. It guided me in handling edge cases such as invalid input, insufficient stock, and full cart scenarios.

Even though AI was used as a guide, all code was manually written and understood by me. I did not copy-paste blindly. I made sure every part of the system was implemented with my own understanding and learning.

📌 Summary

This project demonstrates:

Object-Oriented Programming (OOP)
Array and cart management
Input validation
Real-world checkout system logic
Basic inventory system simulation
