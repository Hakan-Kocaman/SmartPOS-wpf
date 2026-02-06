# 🧾 SmartPOS (WPF Desktop Point of Sale System)

SmartPOS is a desktop point-of-sale (POS) automation application developed with **C# and WPF**.  
The system simulates a real-world cashier workflow with product management, cart operations, order processing, and multi-window GUI interaction.

---

## 🚀 Features

- 🛒 Dynamic Cart System
- 📦 Product Listing & Order Management
- 🪟 Multi-Window Interface (User & Customer Menus)
- 🔄 Real-Time UI Updates
- 💾 SQL Server Database Integration
- 🔐 Session & Logout Control
- 📅 Date-based Order Navigation

---

## 🛠️ Technologies Used

- C#
- WPF (.NET)
- XAML
- SQL Server
- ADO.NET

---

---

## 📌 Notes

- Developed using a code-behind architecture.
- Focused on GUI lifecycle management and practical POS workflow simulation.
- Built as a desktop software engineering project.

---

## 👨‍💻 Author

Hakan Kocaman


## 📸 Menus
![WhatsApp Image 2026-02-06 at 21 37 45](https://github.com/user-attachments/assets/3b06f309-a467-4250-bee3-5bf6dd9749bc)

## 🪟 Cashier Menu (UserMenu)

UserMenu is the main cashier interface of the SmartPOS desktop application.  
It allows the cashier to manage daily orders, monitor active transactions, control the customer display screen, and handle session operations through an organized graphical interface.

### Responsibilities
- Open and control the Customer Display Menu
- Refresh order and transaction data
- Handle logout and session operations

### Technical Role

- Serves as the primary cashier control panel
- Manages multi-window communication
- Controls transaction workflow and UI state
- Handles cashier-side interactions in the POS process


![CustomerMenu](https://github.com/user-attachments/assets/953ed85e-dce7-4cd6-8378-2d1427fb0e44)
## 🪟 Customer Display Menu (CustomerMenu)

CustomerMenu is the customer-facing display screen of the SmartPOS desktop application.  
It allows customers to view their current cart, follow order updates in real time, and monitor the total payment amount during the checkout process.

### Responsibilities

- Display active cart items to the customer
- Show real-time order updates
- Present total price and transaction summary
- Provide a clear and simple customer-facing interface
- Synchronize with the Cashier (UserMenu) window

### Technical Role

- Acts as a secondary window controlled by the cashier interface
- Receives live data updates from UserMenu
- Reflects transaction changes instantly
- Enhances transparency during the POS process

![AdminMenu](https://github.com/user-attachments/assets/6d0fd7b3-a6e2-4e12-815b-f357974f1e60)
## 🪟 Admin Panel (AdminMenu)

AdminMenu is the administrative control panel of the SmartPOS desktop application.  
It allows authorized users to monitor system data, and control operational settings through a centralized interface.

### Responsibilities

- Monitor system data and updates
- Control operational configurations
- Maintain product and sales consistency

### Technical Role

- Serves as the administrative management interface
- Handles product and system configuration logic
- Provides data management capabilities
- Supports overall POS system maintenance



---

## 📌 Notes

- Developed using a code-behind architecture.
- Focused on GUI lifecycle management and practical POS workflow simulation.
- Built as a desktop software engineering project.

---

## 👨‍💻 Author

Hakan Kocaman

