using BookXtation.DAL.Models.Data;
using Stripe;

namespace BooXtation.Helpers
{
    internal static class HtmlText
    {
        

        internal static string CreateHTMLtext(List<Order_Item> order_Items, string userName)
        {

            string result = $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            line-height: 1.6;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 80%;
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #fff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
                        }}
                        h2 {{
                            color: #4CAF50;
                            text-align: center;
                        }}
                        p {{
                            font-size: 16px;
                            margin-bottom: 20px;
                        }}
                        table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 20px;
                        }}
                        th, td {{
                            padding: 12px;
                            text-align: left;
                            border-bottom: 1px solid #ddd;
                        }}
                        th {{
                            background-color: #4CAF50;
                            color: white;
                        }}
                        td {{
                            background-color: #f9f9f9;
                        }}
                        .total {{
                            font-size: 18px;
                            font-weight: bold;
                            color: #333;
                        }}
                        .footer {{
                            text-align: center;
                            color: #777;
                            font-size: 14px;
                            margin-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Order Details</h2>
                        <p>Thank you for your purchase, {userName}.</p>
                         <table>
                            <tr>
                                <th>Order ID</th
                                <th>Order Status</th>
                                <th>Payment Method</th>
                                <th>Payment Status</th>
                            </tr>
                            <tr>
                                <td>{order_Items[0].Order.Order_ID}</td>
                                <td>{order_Items[0].Order.OrderStatus}</td>
                                <td>{order_Items[0].Order.Payment.PaymentMethod}</td>
                                <td>{order_Items[0].Order.PaymentStatus}</td>
                
                            </tr>
                        </table>
                        <table>
                            <tr>
                                
                                <th>Book Name</th
                                <th>Quantity</th>
                                <th>Amount</th>
                                
                            </tr>";
            foreach (var item in order_Items)
            {
                result += $@"<tr>
                                    
                                    <td>{item.book.Title}</td>
                                    <td>{item.Quantity}</td>
                                    <td>$ {item.Price}</td>
                                </tr>";
            };
            result += $@"</table>
                        <p>If you have any questions, feel free to contact us.</p>
                        <div class='footer'>
                            <p>Best regards,</p>
                            <p>BooXtation</p>
                        </div>
                    </div>
                </body>
            </html>";
            return result;
        }

        internal static string CreateHTMLtext(Charge charge , List<Order_Item> order_Items, string userName)
        {

            string result = $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            line-height: 1.6;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 80%;
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #fff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
                        }}
                        h2 {{
                            color: #4CAF50;
                            text-align: center;
                        }}
                        p {{
                            font-size: 16px;
                            margin-bottom: 20px;
                        }}
                        table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 20px;
                        }}
                        th, td {{
                            padding: 12px;
                            text-align: left;
                            border-bottom: 1px solid #ddd;
                        }}
                        th {{
                            background-color: #4CAF50;
                            color: white;
                        }}
                        td {{
                            background-color: #f9f9f9;
                        }}
                        .total {{
                            font-size: 18px;
                            font-weight: bold;
                            color: #333;
                        }}
                        .footer {{
                            text-align: center;
                            color: #777;
                            font-size: 14px;
                            margin-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Order Details</h2>
                        <p>Thank you for your purchase, {userName}.</p>
                         <table>
                            <tr>
                                <th>Order ID</th
                                <th>Order Status</th>
                                <th>Payment Method</th>
                                <th>Payment Status</th>
                            </tr>
                            <tr>
                                <td>{order_Items[0].Order.Order_ID}</td>
                                <td>{order_Items[0].Order.OrderStatus}</td>
                                <td>{order_Items[0].Order.Payment.PaymentMethod}</td>
                                <td>{order_Items[0].Order.PaymentStatus}</td>
                
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <th>Reciept ID</th
                                <th>Status</th>
                                <th>Amount</th>
                                <th>Date</th>
                            </tr>
                            <tr>
                                <td>{charge.Id}</td>
                                <td>{charge.Status}</td>
                                <td>$ {charge.Amount / 100}</td>
                                <td>{charge.Created}</td>
                
                            </tr>
                        </table>
                        <table>
                            <tr>
                                
                                <th>Book Name</th
                                <th>Quantity</th>
                                <th>Amount</th>
                                
                            </tr>";
            foreach (var item in order_Items)
            {
                result += $@"<tr>
                               <td>{item.book.Title}</td>
                               <td>{item.Quantity}</td>
                               <td>$ {item.Price}</td>
                             </tr>";
            };

            result += $@"</table>
                        <p>If you have any questions, feel free to contact us.</p>
                        <div class='footer'>
                            <p>Best regards,</p>
                            <p>BooXtation</p>
                        </div>
                    </div>
                </body>
            </html>";
            return result;
        }
    }
}
