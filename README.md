# 🛒 Ian Adelante Shopping Cart System (Part 2)

This is a console-based Shopping Cart System built in C#.  
It allows users to browse products, manage a shopping cart, and complete checkout with payment validation, receipt generation, and inventory tracking.

The system also includes product search, order history, and low stock alerts.

---

# 🚀 Part 2 Features

## 🛍️ Product Management
- View all products
- Each product includes:
  - ID
  - Name
  - Category
  - Price
  - Stock quantity

---

## 🔎 Product Search
- Search products by name
- Case-insensitive matching
- Displays matching product details

---

## 🛒 Cart Management System
- Add items to cart
- View cart contents
- Remove specific items
- Clear entire cart
- Prevents invalid stock purchases
- Automatically updates stock when cart changes

---

## 💳 Checkout System
- Displays complete receipt
- Includes:
  - Receipt number
  - Date and time
  - Purchased items
  - Grand total
  - Discount (10% if ₱5000 or more)
  - Final total

---

## 💰 Payment Validation
- Ensures numeric input only
- Rejects invalid or insufficient payment
- Re-prompts until valid payment is entered
- Calculates correct change

---

## 📦 Stock Management
- Deducts stock after purchase
- Restores stock when items are removed or cart is cleared
- Prevents out-of-stock purchases

---

## ⚠️ Low Stock Alert
- Displays products with stock 5 or below
- Helps identify items for restocking

---

## 📜 Order History
- Stores completed transactions during runtime
- Displays past receipts with totals

---

# 🤖 AI Usage

I used AI as a learning and development tool throughout this project. It helped me understand programming concepts, debug errors, and improve the overall structure of my Shopping Cart System.

AI assisted me in breaking down complex logic such as:
- Cart management system design
- Stock validation and updates
- Payment validation and receipt computation
- Program flow and menu structure
- Handling edge cases like invalid input and empty cart scenarios

When I encountered errors, AI helped explain the cause so I could fix them myself instead of just copying answers. This improved my understanding of C# programming and problem-solving skills.

I ensured that all code was manually written and understood. AI was only used as a guide for learning, debugging, and improving my programming logic.
