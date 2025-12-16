window.checkout = (stripePublickKey,sessionId) => {
    let stripe = Stripe(sessionId);
    stripe.redirectToCheckout({
        sessionId : sessionId
    });
}