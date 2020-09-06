using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        JoystickVM joystick;

        double x, y, xN, yN;
        const double XPOSITION = 0;
        const double YPOSITION = 0;
        const double MAXDIFFERENCE = 40;
        bool mousePressed;

        double xPosition;
        double yPosition;
        double xDelta;
        double yDelta;

        public Joystick(IModel model)
        {
            joystick = new JoystickVM(model);
            InitializeComponent();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseDown(sender, e);
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Ellipse_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseDown(sender, e);
        }

        private void Ellipse_MouseMove_1(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Ellipse_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseDown(sender, e);
        }

        private void Ellipse_MouseMove_2(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseDown(sender, e);
        }

        private void Path_MouseMove(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Path_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Ellipse_MouseMove_3(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseUp_3(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        /// <summary>
        /// do  the protocol as the mouse up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            mousePressed = false;
            // move back the gray circle
            RenderTransformOrigin.Offset(0.5, 0.5);
            knobPosition.X = XPOSITION;
            knobPosition.Y = YPOSITION;
        }

        private void Ellipse_MouseMove_4(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseUp_4(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        /// <summary>
        /// move the gray circle to the center
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnobBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            RenderTransformOrigin.Offset(0.5, 0.5);
            knobPosition.X = XPOSITION;
            knobPosition.Y = YPOSITION;
        }

        private void Path_MouseMove_1(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Path_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Path_MouseMove_2(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Path_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Path_MouseMove_3(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Path_MouseUp_3(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void Path_MouseMove_4(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Path_MouseUp_4(object sender, MouseButtonEventArgs e)
        {
            KnobBase_MouseUp(sender, e);
        }

        private void KnobBase_MouseEnter(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseEnter_1(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Ellipse_MouseEnter_2(object sender, MouseEventArgs e)
        {
            KnobBase_MouseMove(sender, e);
        }

        private void Base_MouseEnter(object sender, MouseEventArgs e)
        {
            mousePressed = false;
            // move back the gray circle
            RenderTransformOrigin.Offset(0.5, 0.5);
            knobPosition.X = XPOSITION;
            knobPosition.Y = YPOSITION;
        }

        /// <summary>
        /// send commands to the server that just came from the gray circle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnobBase_MouseMove(object sender, MouseEventArgs e)
        {
            // when the mouse is pressed
            if (mousePressed)
            {
                Rectangle r = (sender as Rectangle);
                // get the circe position
                xN = e.GetPosition(r).X;
                yN = e.GetPosition(r).Y;

                // calculate the delta position
                xDelta = (xN - x);
                yDelta = (yN - y);

                // check if the circle got out of borders
                if (Lenght(XPOSITION, YPOSITION, (xPosition + xDelta), (yPosition + yDelta)) <= MAXDIFFERENCE)
                {
                    // the circle in borderd
                    knobPosition.X = (xPosition + xDelta);
                    knobPosition.Y = (yPosition + yDelta);

                    // normilze the new position
                    double xCahnge = ((xPosition + xDelta) - XPOSITION) / MAXDIFFERENCE;
                    double yCahnge = ((yPosition + yDelta) - YPOSITION) / MAXDIFFERENCE;

                    // send the commands to the server
                    joystick.UpdateDirection(xCahnge, yCahnge);
                }
                else
                {
                    // the circle isn't in borders
                    // get the angle of the circle according to the center
                    double angle = Math.Atan(yDelta / xDelta);

                    // calculate the delta when we put the circle exacly on the border
                    bool SN = (xDelta < 0) ? true : false;
                    xDelta = MAXDIFFERENCE * Math.Cos(angle);
                    yDelta = MAXDIFFERENCE * Math.Sin(angle);

                    if (SN) { xDelta *= -1; yDelta *= -1; }

                    knobPosition.X = (xPosition + xDelta);
                    knobPosition.Y = (yPosition + yDelta);

                    // normilze the new position
                    double xCahnge = ((xPosition + xDelta) - XPOSITION) / MAXDIFFERENCE;
                    double yCahnge = ((yPosition + yDelta) - YPOSITION) / MAXDIFFERENCE;

                    // send the commands to the server
                    joystick.UpdateDirection(xCahnge, yCahnge);
                }
            }
        }

        /// <summary>
        /// get the position of the circle when the mouse is down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnobBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mousePressed = true;
            Rectangle r = (sender as Rectangle);
            x = e.GetPosition(r).X;
            y = e.GetPosition(r).Y;

            xPosition = knobPosition.X;
            yPosition = knobPosition.Y;
        }

        /// <summary>
        /// calculate distance bitween two points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private double Lenght(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Abs((x1 - x2)) * Math.Abs((x1 - x2)) + Math.Abs((y1 - y2)) * Math.Abs((y2 - y1)));
        }
    }
}
