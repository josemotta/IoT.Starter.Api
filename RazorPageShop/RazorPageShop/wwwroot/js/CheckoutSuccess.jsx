class CheckoutSuccessView extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <br />
                <Row>
                    <span className="fa fa-check-circle"></span>
                </Row>                
                <h3 className="text-center">
                    <span>Your order has been received. Thank you for your purchase!</span>
                </h3>
                <h4 className="text-center">
                    <span>Your order # is:</span>
                    <span className="green">{this.props.model.orderNumber}</span>
                </h4>
                <br />
                <h3>
                    Order Information
                </h3>

                <Panel>
                    <Row>
                        <Column md={6}>
                            <h4>
                                <span>Order No:</span>
                                <span className="green">{this.props.model.orderNumber}</span>
                            </h4>
                            <p>
                                You will receive a confirmation e-mail with the details of your order. Please verify your AntiSpam settings of your e-mail provider.
                            </p>
                        </Column>
                        <Column md={6}>
                            <h4>Payment term</h4>
                            <div className="boleto">
                                <p><i className="fa fa-paypal leading-icon" aria-hidden="true"></i> Paypal</p>
                                <p className="offset30"><Dollars val={this.props.model.total} /></p>
                            </div>
                        </Column>
                    </Row>
                    <Row className="gray row-eq-height border-top border-bottom">
                        <Column md={3}>
                            <h4><span className="fa fa-user leading-icon"></span>Your info</h4>
                            <p className="offset30">{this.props.model.customerInfo.CustomerName}</p>
                            <p className="offset30">{this.props.model.customerInfo.PhoneNumber}</p>
                        </Column>
                        <Column md={3} className="border-right">
                            <br />
                            <br />
                            <p>{this.props.model.customerInfo.Email}</p>
                        </Column>
                        <Column md={6}>
                            <h4><span className="fa fa-home leading-icon"></span>Shipping address</h4>
                            <p className="offset30">{this.props.model.customerInfo.DeliveryAddress}</p>
                        </Column>
                    </Row>
                    <Row className="gray">
                        <Column md={6}>
                            <h4><span className="fa fa-gift leading-icon"></span>Delivery</h4>
                        </Column>
                        <Column md={6}>
                            <br />
                            <p className="float-right">
                                Delivery time is {this.props.model.DeliveryUpTo} days
                            </p>
                        </Column>
                    </Row>
                    <Row className="gray">
                        <Column md={6}>
                            <p className="offset30"><b>Product description</b></p>
                        </Column>
                        <Column md={6} className="pull-right">
                            <p><b className="float-right">Quantity</b></p>
                        </Column>
                    </Row>
                    { this.props.model.cartItems.map(item =>
                        <Row className="gray">
                            <Column md={6}>
                                <div className="offset30 truncate">
                                    <span>•</span>
                                    <span>{item.description}</span>
                                </div>
                            </Column>
                            <Column md={6} className="pull-right">
                                <p className="float-right">{item.quantity}</p>
                            </Column>
                        </Row>
                            )
                    }
                </Panel>

                <Row>
                    <Column md={9}></Column>
                    <a href="/">
                        <Column md={2}>
                            <Button bsStyle="success">Back to product catalog</Button>
                        </Column>
                    </a>
                </Row>
            </div>
      );
    }
}

CheckoutSuccessView.propTypes = {

};