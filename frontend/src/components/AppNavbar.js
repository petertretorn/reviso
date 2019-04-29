import React, { Component } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  Container
} from 'reactstrap';

class AppNavbar extends Component {
  state = {
    isOpen: false
  }
 
  toggle = () => {
    this.setState({
      isOpen: !this.state.isOpen
    });
  };

  render() {
      return (
        <div>
            <Navbar color="dark" dark expand="sm" className="mb5">
                <Container>
                    <NavbarBrand href="/">Reviso</NavbarBrand>
                    <NavbarToggler onClick={this.toggle} />
                    <Collapse isOpen={this.state.isOpen} navbar>
                        <Nav className="ml-auto" navbar>
                        <NavItem>
                            <NavLink target="_blank" href="https://github.com/petertretorn/reviso">View on Github</NavLink>
                        </NavItem>
                        </Nav>
                    </Collapse>
                </Container>

            </Navbar>
         </div>
      )
  } 

}
export default AppNavbar;